using System.Data.Entity;
using Discipline.Domain.Entities;

namespace Discipline.Domain.Abstract {
    public abstract class AbstractDbContext : DbContext {

        public AbstractDbContext(string connString): base(connString) { }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Duration> Durations { get; set; }
    }
}
