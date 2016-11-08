using System.Data.Entity;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Domain.Concrete {
    public class DbContext : AbstractDbContext {
        public DbContext() : base("Tache") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContext, Migrations.Configuration>("Tache"));
        }
    }
}