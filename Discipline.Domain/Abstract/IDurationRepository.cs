using System;
using System.Linq;
using Discipline.Domain.Entities;

namespace Discipline.Domain.Abstract {
    public interface IDurationRepository {
        IQueryable<Duration> Durations { get; }

        void Update(int durationId, Activity activity, DateTime from, DateTime to);
        Duration Delete(int durationId);
    }
}
