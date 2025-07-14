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
            modelBuilder.Entity<Waiter>().ToTable("waiters");
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Business>()
                .HasOne(b => b.user)
                .WithMany()
                .HasForeignKey(b => b.user_id);
            modelBuilder.Entity<Waiter>()
                .HasOne(w => w.user)
                .WithMany()
                .HasForeignKey(w => w.user_id);
            modelBuilder.Entity<Waiter>()
                .HasOne(w => w.business)
                .WithMany()
                .HasForeignKey(w => w.business_id);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.business)
                .WithMany()
                .HasForeignKey(p=>p.business_id);

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Business> Business => Set<Business>();
        public DbSet<Waiter> Waiters => Set<Waiter>();
        public DbSet<Product> Products => Set<Product>();
    }
}
