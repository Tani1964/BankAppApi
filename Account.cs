using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string AccountNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string AccountName { get; set; } = string.Empty;

    // Add Name property (alias for AccountName for backward compatibility)
    [NotMapped]
    public string Name
    {
        get => AccountName;
        set => AccountName = value;
    }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; } = 0;

    [Required]
    public string AccountType { get; set; } = "Savings"; // Savings, Current, etc.

    [Required]
    [StringLength(3)]
    public string Currency { get; set; } = "NGN"; // Default to Nigerian Naira

    [Required]
    [StringLength(4)]
    public string Pin { get; set; } = string.Empty; // 4-digit PIN

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    // Foreign key to User
    [Required]
    public string UserId { get; set; } = string.Empty;

    // Navigation property
    public virtual User User { get; set; } = null!;

    // Navigation property for transactions
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
