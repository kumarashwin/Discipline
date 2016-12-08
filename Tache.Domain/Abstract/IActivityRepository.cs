using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Entities;

namespace Tache.Domain.Abstract {
    public interface IActivityRepository {
        IQueryable<Activity> Activities { get; }

        Activity Retrieve(int id);
        void CreateOrUpdate(Activity activity);
        Activity Delete(int activityId);
        Activity Hide(int activityId);

        void Start(int activity, DateTime clientRequestTime);
        void Stop(int activity, DateTime clientRequestTime);
    }
}
