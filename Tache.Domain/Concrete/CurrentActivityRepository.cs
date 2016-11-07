using System;
using System.Linq;
using Tache.Domain.Entities;

namespace Tache.Domain.Concrete {
    public class CurrentActivityRepository {
        private DbContext context;

        public CurrentActivityRepository(DbContext context = null) {
            if (context == null) context = new DbContext();
            this.context = context;
        }

        public IQueryable<CurrentActivity> CurrentActivities { get { return context.CurrentActivities; } }

        public void Start(Activity activity, DateTime clientRequestTime) {
            context.CurrentActivities.Add(new CurrentActivity { ActivityId = activity.Id, Start = clientRequestTime });
            context.SaveChanges();
        }

        public void Stop(Activity activity, DateTime clientRequestTime) {
            var currentActivity = context.CurrentActivities.Where(a => a.ActivityId == activity.Id).First();
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
