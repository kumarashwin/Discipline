using System;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Domain.Concrete {
    public class CurrentActivityRepository : ICurrentActivityRepository {
        private AbstractDbContext context;

        public CurrentActivityRepository(AbstractDbContext context) {
            this.context = context;
        }

        public IQueryable<CurrentActivity> CurrentActivities { get { return context.CurrentActivities; } }

        public void Start(int activity, DateTime clientRequestTime) {
            context.CurrentActivities.Add(new CurrentActivity { ActivityId = activity, Start = clientRequestTime });
            context.SaveChanges();
        }

        public void Stop(int activity, DateTime clientRequestTime) {
            var currentActivity = context.CurrentActivities.Where(a => a.ActivityId == activity).First();
            AddDurations(currentActivity.Start, clientRequestTime, currentActivity.ActivityId);
            context.CurrentActivities.Remove(currentActivity);
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
