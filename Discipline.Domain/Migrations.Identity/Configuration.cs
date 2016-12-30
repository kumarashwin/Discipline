namespace Discipline.Domain.Migrations.Identity {
    using Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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

            string[] users = new string[] { "kumar.ashwin@outlook.com", "katherine.pineault@gmail.com"};
            foreach (string user in users) {
                if (!(context.Users.Any(u => u.UserName == user))) {
                    var applicationUser = new ApplicationUser { UserName = user };
                    userManager.Create(applicationUser, "Password@123");
                    userManager.AddToRole(applicationUser.Id, "User");
                }
            }

            var admin = new ApplicationUser { UserName = "admin@discipline.com" };
            userManager.Create(admin, "Password@123");
            userManager.AddToRole(admin.Id, "Admin");
        }
    }
}
