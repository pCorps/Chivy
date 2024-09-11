using ChivyPoo.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChivyPoo.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        { 
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Appetizer" },
                new Category { Id = 2, Name = "Entree" },
                new Category { Id = 3, Name = "Dessert" }
                );
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql("Host=localhost;Database=my_post_db;Username=postgres;Password=3rdCorp$");
        //        //Host = my_host; Database = my_db; Username = my_user; Password = my_pw
        //    }
        //}

    }
}
