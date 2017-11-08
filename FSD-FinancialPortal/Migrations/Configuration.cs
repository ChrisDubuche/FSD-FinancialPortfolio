namespace FSD_FinancialPortal.Migrations
{
    using FSD_FinancialPortal.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            #region Role Creation - per deliverables

            var roleManager = new RoleManager<IdentityRole>(
               new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Head"))
            {
                roleManager.Create(new IdentityRole { Name = "Head" });
            }

            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }            

            if (!context.Roles.Any(r => r.Name == "Guest"))
            {
                roleManager.Create(new IdentityRole { Name = "Guest" });
            }

            #endregion

            #region Assign User to Role 
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "test@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "test@mailinator.com",
                    Email = "test@mailinator.com",
                    FirstName = "Test",
                    LastName = "Testor",
                    DisplayName = "Seeded Admin"
                }, "Abc&123!");
            }

            var userId = userManager.FindByEmail("test@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            #endregion
        }
    }
}
