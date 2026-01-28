using HabitTracker.Application.DTOs;

namespace HabitTracker.Application.Interfaces;

public interface IAuthService
{
    LoginResponseDto Login(string username);
    CurrentUserDto GetCurrentUser(string? token);
    void Logout(string? token);
}
