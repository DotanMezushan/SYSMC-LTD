using Microsoft.EntityFrameworkCore;
using SYSMCLTD_API.Entities;

namespace SYSMCLTD_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // קשר בין Address ל-Customer
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // קשר בין Contact ל-Customer
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Customer)
                .WithMany(cu => cu.Contacts)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
