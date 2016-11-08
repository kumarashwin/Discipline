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
            if(activity.Id == 0) {
                context.Activities.Add(activity);
            } else {
                var dbEntry = context.Activities.Find(activity.Id);
                if(dbEntry != null) {
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
    }
}
