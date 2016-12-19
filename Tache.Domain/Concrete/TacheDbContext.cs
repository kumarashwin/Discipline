using System.Data.Entity;
using Tache.Domain.Abstract;

namespace Tache.Domain.Concrete {
    public class TacheDbContext : AbstractDbContext {

        public TacheDbContext() : base("Tache") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TacheDbContext, Migrations.Configuration>("Tache"));
        }
    }
}