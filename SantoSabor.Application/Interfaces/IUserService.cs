using SantoSabor.Application.DTOs;

namespace SantoSabor.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<Guid> CreateUserAsync(UserCreateDTO userDto);
        Task<bool> UpdateUserAsync(Guid id, UserUpdateDTO userUpdateDTO);
        Task<bool> DeleteUserAsync(Guid id);

        Task<UserDTO?> GetUserByEmailAsync(string email);
    }
}