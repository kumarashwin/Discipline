using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Domain.Concrete {
    public class ActivityRepository : IActivityRepository {
        private AbstractDbContext context;

        public ActivityRepository(AbstractDbContext context) {
            this.context = context;
        }

        public IQueryable<Activity> Activities {
            get {
                return context.Activities;
            }
        }

        public void CreateOrUpdate(Activity activity) {
            if (activity.Id == 0) {
                context.Activities.Add(activity);
            } else {
                var dbEntry = context.Activities.Find(activity.Id);
                if (dbEntry != null) {
                    dbEntry.Name = activity.Name;
                    dbEntry.Description = activity.Description;
                }
            }
            context.SaveChanges();
        }

        public Activity Delete(int activityId) {
            var dbEntry = context.Activities.Find(activityId);
            if (dbEntry != null) {
                context.Activities.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void Start(int activity, DateTime clientRequestTime) {
            context.Activities.Where(a => a.Id == activity).First().Start = clientRequestTime;
            context.SaveChanges();
        }

        public void Stop(int activity, DateTime clientRequestTime) {
            Activity currentActivity = context.Activities.Where(a => a.Id == activity).First();
            AddDurations((DateTime)currentActivity.Start, clientRequestTime, currentActivity.Id);
            currentActivity.Start = null;
            context.SaveChanges();
        }

        private void AddDurations(DateTime startTime, DateTime stopTime, int activityId) {
            DateTime midnight = startTime.Date.AddDays(1);              // Gives us 00:00, the next day
            if (stopTime >= midnight) {                                 // stopTime happens the next day
                AddDurations(midnight, stopTime, activityId);           // Call recursively until stopTime is in the same day
                stopTime = midnight.AddSeconds(-1);
            }
            context.Durations.Add(new Duration { ActivityId = activityId, From = startTime, To = stopTime });
        }
    }
}
