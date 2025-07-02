using BankApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountDbContext _context;

    public AccountController(AccountDbContext context)
    {
        _context = context;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<List<Account>>> GetAccounts(int userId)
    {
        var accounts = await _context.Accounts.Where(a => a.Id == userId).ToListAsync();
        if (accounts == null || !accounts.Any())
        {
            return NotFound("No accounts found for this user.");
        }
        return Ok(accounts);
    }

    [HttpPost]
    public async Task<ActionResult<Account>> CreateAccount(Account account)
    {
        if (account == null)
        {
            return BadRequest("Account data is required.");
        }

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAccounts), new { userId = account.Id }, account);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccount(int id, Account account)
    {
        if (id != account.Id)
        {
            return BadRequest("Account ID mismatch.");
        }
        if (account == null)
        {
            return BadRequest("Account data is required.");
        }
        var existingAccount = await _context.Accounts.FindAsync(id);
        if (existingAccount == null)
        {
            return NotFound("Account not found.");
        }
        existingAccount.Balance = account.Balance;
        existingAccount.Name = account.Name;
        existingAccount.Currency = account.Currency;
        existingAccount.Pin = account.Pin;

        _context.Entry(existingAccount).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
        {
            return NotFound("Account not found.");
        }
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
