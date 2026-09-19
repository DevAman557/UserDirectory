using UserDirectory.Api.DTOs;
using UserDirectory.Api.Models;
using UserDirectory.Api.Repositories.Interfaces;
using UserDirectory.Api.Services.Interfaces;

namespace UserDirectory.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(MapToResponse);
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            return null;

        return MapToResponse(user);
    }

    public async Task<UserResponseDto> CreateAsync(
        CreateUserDto request)
    {
        var user = new User
        {
            Name = request.Name.Trim(),
            Age = request.Age,
            City = request.City.Trim(),
            State = request.State.Trim(),
            Pincode = request.Pincode.Trim(),
            Version = 1
        };

        var createdUser = await _userRepository.AddAsync(user);

        return MapToResponse(createdUser);
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateUserDto request)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            return false;

        user.Name = request.Name.Trim();
        user.Age = request.Age;
        user.City = request.City.Trim();
        user.State = request.State.Trim();
        user.Pincode = request.Pincode.Trim();

        await _userRepository.UpdateAsync(user);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            return false;

        await _userRepository.DeleteAsync(user);

        return true;
    }

    private static UserResponseDto MapToResponse(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Age = user.Age,
            City = user.City,
            State = user.State,
            Pincode = user.Pincode,
            Version = user.Version
        };
    }
}