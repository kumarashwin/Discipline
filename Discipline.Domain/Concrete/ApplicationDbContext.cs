using System.Data.Entity;
using Discipline.Domain.Abstract;

namespace Discipline.Domain.Concrete {
    public class ApplicationDbContext : AbstractDbContext {

        public ApplicationDbContext() : base("DefaultConnection") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>("DefaultConnection"));
        }
    }
}