using Microsoft.EntityFrameworkCore;
using RestoBackEnd.Models;

namespace RestoBackEnd.Data
{
    public class RestoDbContext : DbContext
    {
        public RestoDbContext(DbContextOptions<RestoDbContext> options) : base(options)
        {
        }

        // DbSets for all models
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Table> Tables { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial data
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, Name = "طاولة 1", IsOccupied = false, Capacity = 4 },
                new Table { Id = 2, Name = "طاولة 2", IsOccupied = false, Capacity = 2 },
                new Table { Id = 3, Name = "طاولة 3", IsOccupied = false, Capacity = 6 },
                new Table { Id = 4, Name = "طاولة 4", IsOccupied = false, Capacity = 4 },
                new Table { Id = 5, Name = "طاولة 5", IsOccupied = false, Capacity = 4 },
                new Table { Id = 6, Name = "طاولة 6", IsOccupied = false, Capacity = 8 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "شاورما دجاج", Price = 3.00m, Quantity = 50, Category = "main", Description = "شاورما دجاج طازجة مع الخضار" },
                new Product { Id = 2, Name = "شاورما لحم", Price = 4.00m, Quantity = 40, Category = "main", Description = "شاورما لحم بقري مع المخللات" },
                new Product { Id = 3, Name = "برجر لحم", Price = 3.50m, Quantity = 30, Category = "main", Description = "برجر لحم مع الجبنة والخضار" },
                new Product { Id = 4, Name = "بطاطا مقلية", Price = 1.50m, Quantity = 100, Category = "appetizers", Description = "بطاطا مقرمشة" },
                new Product { Id = 5, Name = "سلطة يونانية", Price = 2.00m, Quantity = 20, Category = "appetizers", Description = "سلطة مع الجبنة الفيتا والزيتون" },
                new Product { Id = 6, Name = "حمص", Price = 1.00m, Quantity = 25, Category = "appetizers", Description = "حمص طحينة مع زيت الزيتون" },
                new Product { Id = 7, Name = "بيبسي", Price = 0.50m, Quantity = 200, Category = "drinks", Description = "مشروب غازي 330 مل" },
                new Product { Id = 8, Name = "عصير برتقال", Price = 1.50m, Quantity = 50, Category = "drinks", Description = "عصير برتقال طازج" },
                new Product { Id = 9, Name = "ماء", Price = 0.25m, Quantity = 300, Category = "drinks", Description = "زجاجة ماء 500 مل" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "أحمد", Role = "Waiter" },
                new Employee { Id = 2, Name = "سارة", Role = "Waiter" },
                new Employee { Id = 3, Name = "خالد", Role = "Chef" },
                new Employee { Id = 4, Name = "ليلى", Role = "Manager" }
            );
        }
    }
}
