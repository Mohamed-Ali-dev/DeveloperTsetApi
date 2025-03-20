using DeveloperTestApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTestApi.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts");
                entity.HasKey(e => e.ACC_Number);
                entity.Property(e => e. ACC_Number)
                .HasColumnName("ACC_Number")
                .HasMaxLength(10)
                .IsRequired();

                entity.Property(e => e.ACC_Parent)
                .HasColumnName("ACC_Parent")
                .HasMaxLength(10);

                entity.Property(e => e.Balance)
                .HasColumnName("Balance")
                .HasColumnType("decimal(20,9");

                entity.HasOne(e => e.ParentAccount)
                .WithMany(e => e.ChildAccounts)
                .HasForeignKey(e => e.ACC_Parent)
                .HasConstraintName("FK_Account_Account");

            });
        }
    }
}
