using System.Data.Entity;
using Tache.Domain.Entities;

namespace Tache.Domain.Concrete {
    public class EFDbContext : DbContext {

        public EFDbContext() : base("Tache") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, Tache.Domain.Migrations.Configuration>("Tache"));
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Duration> Durations { get; set; }
        public DbSet<CurrentActivity> CurrentActivities { get; set; }
        public DbSet<Budget> Budgets { get; set; }
    }
}
