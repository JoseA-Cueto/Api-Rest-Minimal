using ApiRestMinimal.Contracts.DTOs;
using ApiRestMinimal.Models;

namespace ApiRestMinimal.Common.Interfaces.Users
{
    public interface IUserService
    {
        Task<User?> AuthenticateUserAsync(LoginDTOs loginDto);
        Task<UserDTOs> RegisterUserAsync(UserDTOs userDto);
        Task<User?> GetUserByIdAsync(Guid id);
    }
}
