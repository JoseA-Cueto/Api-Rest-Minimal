using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Common.Interfaces.Users;
using ApiRestMinimal.Data;
using ApiRestMinimal.Models;
using ApiRestMinimal.Persistence.Base;
using Microsoft.EntityFrameworkCore;

namespace ApiRestMinimal.Persistence.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task CreateUserAsync(User user)
        {
           _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async  Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
