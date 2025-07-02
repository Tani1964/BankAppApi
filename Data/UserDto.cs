using Microsoft.EntityFrameworkCore;

namespace BankApp.Data
{
    public class UserDto : DbContext
    {
        public UserDto(DbContextOptions<UserDto> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
