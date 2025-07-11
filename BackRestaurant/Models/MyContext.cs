using Microsoft.EntityFrameworkCore;

namespace BackRestaurant.Models
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Business>().ToTable("business");
            modelBuilder.Entity<Business>()
                .HasOne(b => b.user)
                .WithMany()
                .HasForeignKey(b => b.user_id);

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Business> Business => Set<Business>();
    }
}
