using FinanceManager.API.Domain.Entities;
using FinanceManager.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.API.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                return;
            }

            modelBuilder.Entity<User>(entity =>
            {
                entity.Navigation(l => l.Transactions)
                      .AutoInclude();

                entity.Property(u => u.Name)
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .HasMaxLength(150);

                entity.Property(u => u.Password)
                      .IsRequired();

                entity.Property(u => u.Role)
                      .HasConversion<string>()
                      .HasMaxLength(50)
                      .IsRequired();

                // Seed users
                entity.HasData(
                    new User
                    {
                        Id = 1,
                        Name = "Admin User",
                        Email = "admin@finance.com",
                        Password = "Admin123!",
                        Role = UserRole.Admin
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Regular User",
                        Email = "user@finance.com",
                        Password = "User123!",
                        Role = UserRole.Standard
                    }
                );
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasOne(t => t.User)
                      .WithMany(u => u.Transactions)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // Delete transactions if user is deleted

                entity.Property(t => t.Amount)
                      .IsRequired()
                      .HasPrecision(18, 2);

                entity.Property(t => t.Type)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(t => t.Category)
                      .HasMaxLength(50);

                entity.Property(t => t.Date)
                      .IsRequired();

                entity.Property(t => t.Notes)
                      .HasMaxLength(250);

                // Seed transactions
                entity.HasData(
                    new Transaction
                    {
                        Id = 1,
                        UserId = 2,
                        Amount = 150.00m,
                        Type = TransactionType.Income,
                        Category = "Salary",
                        Date = DateTime.UtcNow.AddDays(-5),
                        Notes = "Monthly paycheck"
                    },
                    new Transaction
                    {
                        Id = 2,
                        UserId = 2,
                        Amount = 45.50m,
                        Type = TransactionType.Expense,
                        Category = "Groceries",
                        Date = DateTime.UtcNow.AddDays(-2),
                        Notes = "Weekly groceries"
                    }
                );
            });
        }
    }
}