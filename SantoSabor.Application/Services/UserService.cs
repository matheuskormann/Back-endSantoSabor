using Microsoft.AspNetCore.Identity;
using SantoSabor.Application.DTOs;
using SantoSabor.Application.Interfaces;
using SantoSabor.Core.Entities;

namespace SantoSabor.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(user => new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            });
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Usuário com Id não foi encontrado!");

            return new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<Guid> CreateUserAsync(UserCreateDTO userDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(userDto.Email);
            if (existingUser != null)
                throw new Exception("Email já cadastrado");

            var identityUser = new IdentityUser
            {
                UserName = userDto.Email,
                Email = userDto.Email
            };

            var result = await _userManager.CreateAsync(identityUser, userDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Erro ao criar usuário: {errors}");
            }

            if (!string.IsNullOrEmpty(userDto.Role))
            {
                await _userManager.AddToRoleAsync(identityUser, userDto.Role);
            }

            try
            {
                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Role = userDto.Role,
                    CreatedAt = DateTime.UtcNow,
                    Password = identityUser.PasswordHash
                };

                await _userRepository.AddAsync(user);
                return user.UserId;
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(identityUser); // rollback
                throw new Exception("Erro ao salvar usuário: " + ex.Message);
            }
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserUpdateDTO userUpdateDTO)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.Name = userUpdateDTO.Name;
            user.Email = userUpdateDTO.Email;
            user.Role = userUpdateDTO.Role;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user);
            return true;
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;

            return new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
