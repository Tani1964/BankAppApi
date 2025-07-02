using Microsoft.AspNetCore.Identity;


public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    
    // Navigation property for accounts
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}