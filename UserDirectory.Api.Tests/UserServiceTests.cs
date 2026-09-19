using Moq;
using UserDirectory.Api.DTOs;
using UserDirectory.Api.Models;
using UserDirectory.Api.Repositories.Interfaces;
using UserDirectory.Api.Services;
using Xunit;

namespace UserDirectory.Api.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _repositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Aman Kumar",
                Age = 30,
                City = "Delhi",
                State = "Delhi",
                Pincode = "110001"
            },
            new User
            {
                Id = 2,
                Name = "Rajni",
                Age = 28,
                City = "Mumbai",
                State = "Maharashtra",
                Pincode = "400001"
            }
        };

        _repositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Aman Kumar", result.First().Name);

        _repositoryMock.Verify(
            x => x.GetAllAsync(),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserExists_ShouldReturnUser()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "Aman Kumar",
            Age = 30,
            City = "Delhi",
            State = "Delhi",
            Pincode = "110001"
        };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Aman Kumar", result.Name);
        Assert.Equal(30, result.Age);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(1),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.GetByIdAsync(99))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.GetByIdAsync(99);

        // Assert
        Assert.Null(result);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(99),
            Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateAndReturnUser()
    {
        // Arrange
        var request = new CreateUserDto
        {
            Name = "  Aman Kumar  ",
            Age = 30,
            City = "  Delhi ",
            State = " Delhi ",
            Pincode = " 110001 "
        };

        var createdUser = new User
        {
            Id = 1,
            Name = "Aman Kumar",
            Age = 30,
            City = "Delhi",
            State = "Delhi",
            Pincode = "110001",
            Version = 1
        };

        _repositoryMock
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(createdUser);

        // Act
        var result = await _userService.CreateAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Aman Kumar", result.Name);
        Assert.Equal("Delhi", result.City);
        Assert.Equal("Delhi", result.State);
        Assert.Equal("110001", result.Pincode);

        _repositoryMock.Verify(
            x => x.AddAsync(It.Is<User>(u =>
                u.Name == "Aman Kumar" &&
                u.City == "Delhi" &&
                u.State == "Delhi" &&
                u.Pincode == "110001" &&
                u.Age == 30 &&
                u.Version == 1)),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenUserExists_ShouldReturnTrue()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "Old Name",
            Age = 25,
            City = "Delhi",
            State = "Delhi",
            Pincode = "110001"
        };

        var request = new UpdateUserDto
        {
            Name = "New Name",
            Age = 30,
            City = "Mumbai",
            State = "Maharashtra",
            Pincode = "400001",
            Version = 1
        };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.UpdateAsync(1, request);

        // Assert
        Assert.True(result);

        Assert.Equal("New Name", user.Name);
        Assert.Equal(30, user.Age);
        Assert.Equal("Mumbai", user.City);
        Assert.Equal("Maharashtra", user.State);
        Assert.Equal("400001", user.Pincode);

        _repositoryMock.Verify(
            x => x.UpdateAsync(user),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenUserDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var request = new UpdateUserDto
        {
            Name = "New Name",
            Age = 30,
            City = "Mumbai",
            State = "Maharashtra",
            Pincode = "400001",
            Version = 1
        };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(99))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.UpdateAsync(99, request);

        // Assert
        Assert.False(result);

        _repositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<User>()),
            Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_WhenUserExists_ShouldReturnTrue()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "Aman Kumar",
            Age = 30,
            City = "Delhi",
            State = "Delhi",
            Pincode = "110001"
        };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.DeleteAsync(1);

        // Assert
        Assert.True(result);

        _repositoryMock.Verify(
            x => x.DeleteAsync(user),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenUserDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.GetByIdAsync(99))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.DeleteAsync(99);

        // Assert
        Assert.False(result);

        _repositoryMock.Verify(
            x => x.DeleteAsync(It.IsAny<User>()),
            Times.Never);
    }
}