namespace Discipline.Domain.Migrations.Identity {
    using Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Discipline.Domain.Concrete.IdentityDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations.Identity";
        }

        protected override void Seed(Discipline.Domain.Concrete.IdentityDbContext context) {
            context.Roles.AddOrUpdate(i => i.Name, new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = "Admin" });
            context.Roles.AddOrUpdate(i => i.Name, new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = "User" });

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            string[] users = new string[] { "test@discipline.com" };
            foreach (string user in users) {
                if (!(context.Users.Any(u => u.UserName == user))) {
                    var applicationUser = new ApplicationUser { UserName = user, TimeZone = "Eastern Standard Time"};
                    userManager.Create(applicationUser, "Password");
                    userManager.AddToRole(applicationUser.Id, "User");
                }
            }
        }
    }
}
