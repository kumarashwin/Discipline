using System;
using System.Linq;
using Discipline.Domain.Abstract;
using Discipline.Domain.Entities;

namespace Discipline.Domain.Concrete {
    public class DurationRepository : IDurationRepository {
        private AbstractDbContext context;

        public DurationRepository(AbstractDbContext context) {
            this.context = context;
        }

        public IQueryable<Duration> Durations { get { return context.Durations; } }

        public void Update(int durationId, Activity activity, DateTime from, DateTime to) {
            var dbEntry = context.Durations.Find(durationId);
            if(dbEntry != null) {
                dbEntry.ActivityId = activity.Id;
                dbEntry.From = from;
                dbEntry.To = to;
            }
            context.SaveChanges();
        }

        public Duration Delete(int durationId) {
            var dbEntry = context.Durations.Find(durationId);
            if (dbEntry != null) {
                context.Durations.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
