using System;
using System.Linq;
using Tache.Domain.Entities;

namespace Tache.Domain.Abstract {
    public interface IDurationRepository {
        IQueryable<Duration> Durations { get; }

        void Update(int durationId, Activity activity, DateTime from, DateTime to);
        Duration Delete(int durationId);
    }
}
