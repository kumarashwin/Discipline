using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Discipline.Domain.Abstract;
using Discipline.Domain.Entities;

namespace Discipline.Domain.Concrete {
    public class ActivityRepository : IActivityRepository {
        private AbstractDbContext context;
        private string userName;

        public ActivityRepository(AbstractDbContext context) {
            this.context = context;
            this.userName = HttpContext.Current.User.Identity.Name;
        }

        public IQueryable<Activity> Activities() => context.Activities.Where(a => a.UserName == userName);

        public IQueryable<Activity> Activities(bool onlyNonCurrentActivites = false) {
            if (onlyNonCurrentActivites) {
                return context.Activities.Where(a => a.UserName == userName && a.Hide == false && a.Start == null);
            }
            return context.Activities.Where(a => a.UserName == userName && a.Hide == false);
        }

        public Activity Retrieve(int id) => context.Activities.Find(id); 

        public void CreateOrUpdate(Activity activity) {
            if (activity.Id == 0) {
                context.Activities.Add(activity);
            } else {
                context.Activities.Attach(activity);
                context.Entry(activity).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        // Shouldn't be ever used by the user
        private Activity Delete(int activityId) {
            var dbEntry = context.Activities.Find(activityId);
            if (dbEntry != null) {
                context.Activities.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        // Use this instead to make it so that the activity is never usable again
        public Activity Hide(int activityId) {
            var dbEntry = context.Activities.Find(activityId);
            if (dbEntry != null) {
                dbEntry.Hide = true;
                context.SaveChanges();
            }
            return dbEntry;
 
        }

        public void StartNew(int activity, DateTime clientRequestTime) {
            // Stops previous activity
            Activity currentActivity = context.Activities.Where(a => a.UserName == userName && a.Start != null).First();
            AddDurations((DateTime)currentActivity.Start, clientRequestTime, currentActivity.Id);
            currentActivity.Start = null;

            // Starts new activity
            context.Activities.Where(a => a.Id == activity).First().Start = clientRequestTime;
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
