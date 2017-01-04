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
        void StartNew(int activity, DateTime clientRequestTime, TimeZoneInfo userTimeZone);
        void AddDurationsUptoMidnight(DateTime startTime, DateTime midnight, int activityId);
        void UpdateStartUptoLastMidnight(DateTime clientMidnightInUtc);
    }
}
