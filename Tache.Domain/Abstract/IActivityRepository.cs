using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Entities;

namespace Tache.Domain.Abstract {
    public interface IActivityRepository {
        Activity Retrieve(int id);
        void CreateOrUpdate(Activity activity);
        Activity Hide(int activityId);

        IQueryable<Activity> Activities();
        IQueryable<Activity> Activities(bool onlyNonCurrentActivities);
        void StartNew(int activity, DateTime clientRequestTime);
    }
}
