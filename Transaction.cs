using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Transaction
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string TransactionId { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    public int AccountId { get; set; }
    
    [Required]
    public string TransactionType { get; set; } = string.Empty; // Credit, Debit, Transfer
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal BalanceAfter { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [StringLength(100)]
    public string? Reference { get; set; }
    
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    
    // For transfers - destination account
    public int? DestinationAccountId { get; set; }
    
    // Navigation properties
    public virtual Account Account { get; set; } = null!;
    public virtual Account? DestinationAccount { get; set; }
}