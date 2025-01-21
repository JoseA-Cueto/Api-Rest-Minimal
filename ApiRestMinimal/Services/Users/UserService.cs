using ApiRestMinimal.Common.Interfaces.Users;
using ApiRestMinimal.Contracts.DTOs;
using ApiRestMinimal.Custom;
using ApiRestMinimal.Models;

namespace ApiRestMinimal.Services.Users
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Utility _utility;

        public UserService(IUserRepository userRepository, Utility utility)
        {
            _userRepository = userRepository;
            _utility = utility;
        }

        public async Task<User?> AuthenticateUserAsync(LoginDTOs loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            if (user is null) return null;

            var hashedPassword = _utility.EncryptPassword(loginDto.Password);
            return user.Password == hashedPassword ? user : null;
        }

        public async Task<UserDTOs> RegisterUserAsync(UserDTOs userDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(userDto.Email);

            if (existingUser != null)
                throw new Exception("User already exists");

            var hashedPassword = _utility.EncryptPassword(userDto.Password);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                Password = hashedPassword
            };

            await _userRepository.CreateUserAsync(newUser);

            return new UserDTOs
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = string.Empty // No devolvemos la contraseña
            };
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
    }
}

