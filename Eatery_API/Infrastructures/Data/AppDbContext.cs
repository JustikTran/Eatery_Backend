using Eatery_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eatery_API.Infrastructures.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartTopping> CartToppings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderTopping> OrderToppings { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username)
                .IsUnique();
            modelBuilder.Entity<Account>()
               .HasIndex(a => a.Email)
               .IsUnique();
            modelBuilder.Entity<Account>()
               .HasIndex(a => a.PhoneNumber)
               .IsUnique();

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>()
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<User>(u => u.AccountId);

            modelBuilder.Entity<UserAddress>().ToTable("UserAddress");
            modelBuilder.Entity<UserAddress>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAddresses)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<Dish>().ToTable("Dish");

            modelBuilder.Entity<Topping>().ToTable("Topping");

            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId);
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Dish)
                .WithMany(d => d.Carts)
                .HasForeignKey(c => c.DishId);

            modelBuilder.Entity<CartTopping>().ToTable("CartTopping");
            modelBuilder.Entity<CartTopping>()
                .HasOne(ct => ct.Topping)
                .WithMany(t => t.CartToppings)
                .HasForeignKey(ct => ct.ToppingId);
            modelBuilder.Entity<CartTopping>()
                .HasOne(ct => ct.Cart)
                .WithMany(c => c.CartToppings)
                .HasForeignKey(ct => ct.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany(ua => ua.Orders)
                .HasForeignKey(o => o.AddressId);
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Dish)
                .WithMany(d => d.OrderItems)
                .HasForeignKey(oi => oi.DishId);
            modelBuilder.Entity<OrderItem>()
                .HasMany(oi => oi.OrderToppings)
                .WithOne(ot => ot.OrderItem)
                .HasForeignKey(ot => ot.OrderItemId);

            modelBuilder.Entity<OrderTopping>().ToTable("OrderTopping");
            modelBuilder.Entity<OrderTopping>()
                .HasOne(ot => ot.Topping)
                .WithMany(t => t.OrderToppings)
                .HasForeignKey(ot => ot.ToppingId);

            modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethod");
            modelBuilder.Entity<PaymentMethod>()
                .HasMany(pm => pm.Orders)
                .WithOne(o => o.PaymentMethod)
                .HasForeignKey(o => o.PaymentMethodId);
        }
    }
}
