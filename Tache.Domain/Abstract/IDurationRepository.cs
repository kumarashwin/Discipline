using System;
using System.Collections;
using System.Linq;
using Tache.Domain.Entities;

namespace Tache.Domain.Abstract {
    public interface IDurationRepository {
        IQueryable<Duration> Durations { get; }

        void Stop(Activity activity);
        void Start(Activity activity);
        void Update(int durationId, Activity activity, DateTime from, DateTime to);
        Duration Delete(int durationId);
    }
}
