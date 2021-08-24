namespace SMS.Data
{
    using SMS.Data.Models;
    using Microsoft.EntityFrameworkCore;

    // ReSharper disable once InconsistentNaming
    public class SMSDbContext : DbContext
    {
        public SMSDbContext()
        {          
        }

        public DbSet<User> Users { get; init; }

        public DbSet<Cart> Carts { get; init; }
        
        public DbSet<Product> Products { get; init; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<User>(u => u.CartId);

            base.OnModelCreating(modelBuilder);
        }
    }
}