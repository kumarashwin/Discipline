using System;
using System.Linq;
using Tache.Domain.Entities;

namespace Tache.Domain.Abstract {
    public interface ICurrentActivityRepository {
        IQueryable<CurrentActivity> CurrentActivities { get;}
        void Start(int activity, DateTime clientRequestTime);
        void Stop(int activity, DateTime clientRequestTime);
    }
}
