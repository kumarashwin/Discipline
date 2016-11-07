using System.Data.Entity;
using Tache.Domain.Entities;

namespace Tache.Domain.Concrete {
    public class DbContext : System.Data.Entity.DbContext {

        public DbContext() : base("Tache") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContext, Migrations.Configuration>("Tache"));
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Duration> Durations { get; set; }
        public virtual DbSet<CurrentActivity> CurrentActivities { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
    }
}