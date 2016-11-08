using System.Data.Entity;
using Tache.Domain.Entities;

namespace Tache.Domain.Abstract {
    public abstract class AbstractDbContext : DbContext {

        public AbstractDbContext(string connString): base(connString) { }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Duration> Durations { get; set; }
        public virtual DbSet<CurrentActivity> CurrentActivities { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
    }
}
