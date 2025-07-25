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
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<OrderItems>().ToTable("order_items");
            modelBuilder.Entity<Table>().ToTable("tables");
            //Business
            modelBuilder.Entity<Business>()
                .HasOne(b => b.user)
                .WithMany()
                .HasForeignKey(b => b.user_id);
            //Waiter
            modelBuilder.Entity<Waiter>()
                .HasOne(w => w.user)
                .WithMany()
                .HasForeignKey(w => w.user_id);
            modelBuilder.Entity<Waiter>()
                .HasOne(w => w.business)
                .WithMany()
                .HasForeignKey(w => w.business_id);
            //Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.business)
                .WithMany()
                .HasForeignKey(p=>p.business_id);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.category)
                .WithMany()
                .HasForeignKey(p => p.category_id);
            //Table
            modelBuilder.Entity<Table>()
                .HasOne(t => t.business)
                .WithMany()
                .HasForeignKey(t => t.business_id);
            //Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.table)
                .WithMany()
                .HasForeignKey(o => o.table_id);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.waiter)
                .WithMany()
                .HasForeignKey(o => o.waiter_id);
            //OrderItems
            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.order)
                .WithMany()
                .HasForeignKey(oi => oi.order_id);
            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.product)
                .WithMany()
                .HasForeignKey(oi => oi.product_id);

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Business> Business => Set<Business>();
        public DbSet<Waiter> Waiters => Set<Waiter>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItems> OrderItems => Set<OrderItems>();
        public DbSet<Table> Tables => Set<Table>();
    }
}
