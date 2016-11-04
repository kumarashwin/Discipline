using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Entities;

namespace Tache.Domain.Abstract {
    public interface IActivityRepository {
        IQueryable<Activity> Activities { get; }

        void CreateOrUpdate(Activity activity);
        Activity Delete(int activityId);
    }
}
