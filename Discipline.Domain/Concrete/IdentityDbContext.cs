﻿using Microsoft.AspNet.Identity.EntityFramework;
using Discipline.Domain.Entities;

namespace Discipline.Domain.Concrete {

    public class IdentityDbContext : IdentityDbContext<ApplicationUser> {
        public IdentityDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }
        public static IdentityDbContext Create() => new IdentityDbContext();
    }
}