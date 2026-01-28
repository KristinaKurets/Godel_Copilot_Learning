using HabitTracker.Application.DTOs;

namespace HabitTracker.Application.Interfaces;

public interface IUserProfileService
{
    Task<UserProfileDto> GetUserProfileAsync();
}
