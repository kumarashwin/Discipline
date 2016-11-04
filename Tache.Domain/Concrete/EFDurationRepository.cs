using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Domain.Concrete {
    public class EFDurationRepository : IDurationRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Duration> Durations { get { return context.Durations; } }

        public void Stop(Activity activity) {
            var currentActivity = context.CurrentActivities.Where(a => a.ActivityId == activity.Id).First();
            var duration = context.Durations.Find(currentActivity.DurationId);
            var currentTime = DateTime.Now;
            duration.To = currentTime;
            duration.TimeSpent = currentTime.Ticks - duration.From.Ticks;
            context.CurrentActivities.Remove(currentActivity);
            context.SaveChanges();
        }

        public void Start(Activity activity) {
            Duration duration = context.Durations.Add(new Duration {
                ActivityId = activity.Id,
                From = DateTime.Now,
                To = null
            });

            context.CurrentActivities.Add(new CurrentActivity {
                ActivityId = activity.Id,
                Duration = duration
            });

            context.SaveChanges();
        }

        public void Update(int durationId, Activity activity, DateTime from, DateTime to) {
            var dbEntry = context.Durations.Find(durationId);
            if(dbEntry != null) {
                dbEntry.ActivityId = activity.Id;
                dbEntry.From = from;
                dbEntry.To = to;
                dbEntry.TimeSpent = to.Ticks - from.Ticks;
            }
            context.SaveChanges();
        }

        public Duration Delete(int durationId) {
            var dbEntry = context.Durations.Find(durationId);
            if (dbEntry != null) {
                context.Durations.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
