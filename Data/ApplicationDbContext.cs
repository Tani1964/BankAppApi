using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // Add your DbSets here for other entities
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure Account entity
        builder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AccountNumber).IsRequired().HasMaxLength(20);
            entity.Property(e => e.AccountName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Balance).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Currency).IsRequired().HasMaxLength(3);
            entity.Property(e => e.Pin).IsRequired().HasMaxLength(4);
            
            // Configure relationship with User
            entity.HasOne(d => d.User)
                  .WithMany(p => p.Accounts)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Transaction entity
        builder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.BalanceAfter).HasColumnType("decimal(18,2)");
            
            // Configure relationship with Account
            entity.HasOne(d => d.Account)
                  .WithMany(p => p.Transactions)
                  .HasForeignKey(d => d.AccountId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            // Configure relationship with destination account (for transfers)
            entity.HasOne(d => d.DestinationAccount)
                  .WithMany()
                  .HasForeignKey(d => d.DestinationAccountId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}