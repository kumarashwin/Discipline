using System;
using System.Collections.Generic;
using System.Linq;
using Discipline.Domain.Entities;

namespace Discipline.Domain.Abstract {
    public interface IActivityRepository {
        Activity Retrieve(int id);
        Activity Hide(int activityId);

        IQueryable<Activity> Activities();
        IQueryable<Activity> Activities(bool onlyNonCurrentActivities);
        void CreateOrUpdate(Activity activity);
        void StartNew(int activity, DateTime clientRequestTime);
        void AddDurations(DateTime startTime, DateTime stopTime, int activityId);
        void UpdateStartUptoCurrentDate(DateTime dateTime);
    }
}
