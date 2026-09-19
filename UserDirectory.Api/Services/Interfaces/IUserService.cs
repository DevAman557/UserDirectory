using UserDirectory.Api.DTOs;

namespace UserDirectory.Api.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();

    Task<UserResponseDto?> GetByIdAsync(int id);

    Task<UserResponseDto> CreateAsync(CreateUserDto request);

    Task<bool> UpdateAsync(int id, UpdateUserDto request);

    Task<bool> DeleteAsync(int id);
}