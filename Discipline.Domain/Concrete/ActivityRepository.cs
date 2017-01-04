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
            if (activity.UserName == null)
                activity.UserName = userName;

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

        public void StartNew(int activity, DateTime clientRequestTime, TimeZoneInfo userTimeZone) {
            // Find last midnight
            DateTime midnight = TimeZoneInfo.ConvertTimeFromUtc(clientRequestTime, userTimeZone).Date;
            Activity currentActivity = context.Activities.Where(a => a.UserName == userName && a.Start != null).First();

            if(currentActivity.Start.Value < midnight.ToUniversalTime())
                AddDurationsUptoMidnight(currentActivity.Start.Value, midnight.ToUniversalTime(), activity);

            context.Durations.Add(new Duration { ActivityId = currentActivity.Id, From = midnight.ToUniversalTime(), To = clientRequestTime });

            // Stops previous activity
            currentActivity.Start = null;

            // Starts new activity
            context.Activities.Where(a => a.Id == activity).First().Start = clientRequestTime;
            context.SaveChanges();
        }

        public void AddDurations(DateTime startTime, DateTime midnight, DateTime endTime, int activityId) {

        }

        /// <summary></summary>
        /// <param name="startTime">Utc</param>
        /// <param name="midnight">Utc version of 00:00:00</param>
        /// <param name="activityId"></param>
        public void AddDurationsUptoMidnight(DateTime startTime, DateTime midnight, int activityId) {
            DateTime lastMidnight = midnight.AddDays(-1);

            if (startTime < lastMidnight) {
                AddDurationsUptoMidnight(startTime, lastMidnight, activityId);
                startTime = lastMidnight;
            }

            // e.g. from '00:00:00' to '23:59:59', but both in UTC
            context.Durations.Add(new Duration { ActivityId = activityId, From = startTime, To = midnight.AddSeconds(-1) });
        }

        /// <summary>
        /// Makes sure that: if an activity was started on a day prior to 
        /// the client's current day (dateParam + 4 for the initial request)
        /// durations upto midnight will be added to the Durations table.
        /// That activity's Start value will be accordingly updated to 
        /// midnight of the client's current day.
        /// </summary>
        /// <param name="clientMidnightInUtc">00:00 for client time zone for their current day, but expressed in UTC</param>
        public void UpdateStartUptoLastMidnight(DateTime clientMidnightInUtc) {
            Activity lastStartedActivity = context.Activities.Where(a => a.UserName == userName && a.Start != null).First();

            // Activity was started prior to client's current day
            if (lastStartedActivity.Start.Value < clientMidnightInUtc) {
                AddDurationsUptoMidnight(lastStartedActivity.Start.Value, clientMidnightInUtc, lastStartedActivity.Id);

                // Update that activity's Start field in Db
                lastStartedActivity.Start = clientMidnightInUtc;
                context.Activities.Attach(lastStartedActivity);
                context.Entry(lastStartedActivity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
