using HabitTracker.Application.DTOs;
using HabitTracker.Application.Interfaces;
using System.Collections.Concurrent;

namespace HabitTracker.Infrastructure.Services;

public class MockAuthService : IAuthService
{
    private static readonly ConcurrentDictionary<string, string> _tokenStore = new();

    public LoginResponseDto Login(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return new LoginResponseDto
            {
                Username = string.Empty,
                Token = string.Empty,
                Message = "Username is required"
            };
        }

        // Generate a simple mock token (just a GUID for simplicity)
        var token = $"mock_token_{Guid.NewGuid():N}";

        // Store the token -> username mapping
        _tokenStore[token] = username;

        return new LoginResponseDto
        {
            Username = username,
            Token = token,
            Message = "Login successful"
        };
    }

    public CurrentUserDto GetCurrentUser(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return new CurrentUserDto
            {
                Username = string.Empty,
                IsAuthenticated = false
            };
        }

        if (_tokenStore.TryGetValue(token, out var username))
        {
            return new CurrentUserDto
            {
                Username = username,
                IsAuthenticated = true
            };
        }

        return new CurrentUserDto
        {
            Username = string.Empty,
            IsAuthenticated = false
        };
    }

    public void Logout(string? token)
    {
        if (!string.IsNullOrWhiteSpace(token))
        {
            _tokenStore.TryRemove(token, out _);
        }
    }
}
