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

            // Kullan�c� tablosuna bir kay�t ekle ve onu yetkili admin rol�yle e�le�tir
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

            userManager.Create(admin, "Qwe!23"); // Test i�in kullan parola mutlaka de�i�tirilmelidir!!!!!!!!

            // olu�turulan kullan�c�ya admin rol� ver

            userManager.AddToRole(admin.Id, "admin");


        }

        private void CreateRoles(ApplicationDbContext context)
        {
            // rol y�netimi i�in rol ma�azasi�na ba�lan

            var rolestore = new RoleStore<IdentityRole>(context);

            // rol ma�azas�na y�netici ata

            var roleManager = new RoleManager<IdentityRole>(rolestore);

            // eklenecek rollerin bir string listesini olu�turlar�m

            string[] roleNames = { "admin", "user", "quest" };

            // dizi i�inde ki her bir rol� s�rayla db ye ekleyelim

            foreach (var name in roleNames)
            {
                // rol zaten varm� kontrol et!
                if (roleManager.RoleExists(name) == false)
                {
                    // eklemek i�in yeni bir rol nesnesi olu�tur.
                    var role = new IdentityRole { Name = name };

                    // yeni rol� db ye ekle
                    roleManager.Create(role);
                }
            }

        }
    }
}
