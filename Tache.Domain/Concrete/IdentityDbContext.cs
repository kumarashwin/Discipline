using Microsoft.AspNet.Identity.EntityFramework;
using Tache.Domain.Entities;

namespace Tache.Domain.Concrete {

    public class IdentityDbContext : IdentityDbContext<ApplicationUser> {
        public IdentityDbContext() : base("Tache", throwIfV1Schema: false) { }
        public static IdentityDbContext Create() => new IdentityDbContext();
    }
}