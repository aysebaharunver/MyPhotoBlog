namespace MyPhotoBlog.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MyPhotoBlog.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MyPhotoBlog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyPhotoBlog.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.


            // Roller tablosuna rol ekle

            CreateRoles(context);

            // Kullanýcý tablosuna bir kayýt ekle ve onu yetkili admin rolüyle eþleþtir
            CreateAdminUser(context);
        }

        private void CreateAdminUser(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var admin = new ApplicationUser
            {
                Email = "aysebaharunver@hotmail.com",
                UserName = "aysebaharunver@hotmail.com"
            };

            userManager.Create(admin, "Qwe!23"); // Test için kullan parola mutlaka deðiþtirilmelidir!!!!!!!!

            // oluþturulan kullanýcýya admin rolü ver

            userManager.AddToRole(admin.Id, "admin");


        }

        private void CreateRoles(ApplicationDbContext context)
        {
            // rol yönetimi için rol maðazasiýna baðlan

            var rolestore = new RoleStore<IdentityRole>(context);

            // rol maðazasýna yönetici ata

            var roleManager = new RoleManager<IdentityRole>(rolestore);

            // eklenecek rollerin bir string listesini oluþturlarým

            string[] roleNames = { "admin", "user", "quest" };

            // dizi içinde ki her bir rolü sýrayla db ye ekleyelim

            foreach (var name in roleNames)
            {
                // rol zaten varmý kontrol et!
                if (roleManager.RoleExists(name) == false)
                {
                    // eklemek için yeni bir rol nesnesi oluþtur.
                    var role = new IdentityRole { Name = name };

                    // yeni rolü db ye ekle
                    roleManager.Create(role);
                }
            }

        }
    }
}
