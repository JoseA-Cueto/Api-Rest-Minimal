using ApiRestMinimal.Common.Interfaces.BaseRepo;
using ApiRestMinimal.Models;

namespace ApiRestMinimal.Common.Interfaces.Users
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(Guid id);
        Task CreateUserAsync(User user);
    }
}
