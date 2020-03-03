using Microsoft.AspNet.Identity.EntityFramework;
using MyPhotoBlog.Models.Entities;
using System.Data.Entity;

namespace MyPhotoBlog.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }



        public DbSet<Category> Categories { get; set; }
        public DbSet<Photo> Photos { get; set; }





        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}