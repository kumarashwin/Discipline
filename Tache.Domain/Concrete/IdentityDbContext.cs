using Microsoft.AspNet.Identity.EntityFramework;
using Tache.Domain.Entities;

namespace Tache.Domain.Models {

    public class IdentityDbContext : IdentityDbContext<TacheUser> {
        public IdentityDbContext() : base("Tache", throwIfV1Schema: false) { }
        public static IdentityDbContext Create() => new IdentityDbContext();
    }
}