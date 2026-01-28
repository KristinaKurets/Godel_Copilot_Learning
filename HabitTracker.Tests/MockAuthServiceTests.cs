using HabitTracker.Infrastructure.Services;

namespace HabitTracker.Tests;

public class MockAuthServiceTests
{
    [Fact]
    public void Login_WithValidUsername_ReturnsSuccessWithToken()
    {
        // Arrange
        var service = new MockAuthService();
        var username = "TestUser";

        // Act
        var result = service.Login(username);

        // Assert
        Assert.Equal(username, result.Username);
        Assert.NotEmpty(result.Token);
        Assert.StartsWith("mock_token_", result.Token);
        Assert.Equal("Login successful", result.Message);
    }

    [Fact]
    public void Login_WithEmptyUsername_ReturnsError()
    {
        // Arrange
        var service = new MockAuthService();

        // Act
        var result = service.Login("");

        // Assert
        Assert.Empty(result.Username);
        Assert.Empty(result.Token);
        Assert.Equal("Username is required", result.Message);
    }

    [Fact]
    public void Login_WithWhitespaceUsername_ReturnsError()
    {
        // Arrange
        var service = new MockAuthService();

        // Act
        var result = service.Login("   ");

        // Assert
        Assert.Empty(result.Username);
        Assert.Empty(result.Token);
        Assert.Equal("Username is required", result.Message);
    }

    [Fact]
    public void Login_GeneratesUniqueTokensForSameUser()
    {
        // Arrange
        var service = new MockAuthService();
        var username = "TestUser";

        // Act
        var result1 = service.Login(username);
        var result2 = service.Login(username);

        // Assert
        Assert.NotEqual(result1.Token, result2.Token);
        Assert.Equal(username, result1.Username);
        Assert.Equal(username, result2.Username);
    }

    [Fact]
    public void GetCurrentUser_WithValidToken_ReturnsAuthenticatedUser()
    {
        // Arrange
        var service = new MockAuthService();
        var username = "TestUser";
        var loginResponse = service.Login(username);

        // Act
        var result = service.GetCurrentUser(loginResponse.Token);

        // Assert
        Assert.True(result.IsAuthenticated);
        Assert.Equal(username, result.Username);
    }

    [Fact]
    public void GetCurrentUser_WithInvalidToken_ReturnsUnauthenticated()
    {
        // Arrange
        var service = new MockAuthService();

        // Act
        var result = service.GetCurrentUser("invalid_token");

        // Assert
        Assert.False(result.IsAuthenticated);
        Assert.Empty(result.Username);
    }

    [Fact]
    public void GetCurrentUser_WithNullToken_ReturnsUnauthenticated()
    {
        // Arrange
        var service = new MockAuthService();

        // Act
        var result = service.GetCurrentUser(null);

        // Assert
        Assert.False(result.IsAuthenticated);
        Assert.Empty(result.Username);
    }

    [Fact]
    public void GetCurrentUser_WithEmptyToken_ReturnsUnauthenticated()
    {
        // Arrange
        var service = new MockAuthService();

        // Act
        var result = service.GetCurrentUser("");

        // Assert
        Assert.False(result.IsAuthenticated);
        Assert.Empty(result.Username);
    }

    [Fact]
    public void Logout_WithValidToken_RemovesToken()
    {
        // Arrange
        var service = new MockAuthService();
        var username = "TestUser";
        var loginResponse = service.Login(username);

        // Act
        service.Logout(loginResponse.Token);
        var result = service.GetCurrentUser(loginResponse.Token);

        // Assert
        Assert.False(result.IsAuthenticated);
        Assert.Empty(result.Username);
    }

    [Fact]
    public void Logout_WithInvalidToken_DoesNotThrow()
    {
        // Arrange
        var service = new MockAuthService();

        // Act & Assert - should not throw
        service.Logout("invalid_token");
    }

    [Fact]
    public void Logout_WithNullToken_DoesNotThrow()
    {
        // Arrange
        var service = new MockAuthService();

        // Act & Assert - should not throw
        service.Logout(null);
    }

    [Fact]
    public void Login_MultipleUsers_StoredIndependently()
    {
        // Arrange
        var service = new MockAuthService();
        var user1 = "User1";
        var user2 = "User2";

        // Act
        var login1 = service.Login(user1);
        var login2 = service.Login(user2);

        var currentUser1 = service.GetCurrentUser(login1.Token);
        var currentUser2 = service.GetCurrentUser(login2.Token);

        // Assert
        Assert.Equal(user1, currentUser1.Username);
        Assert.Equal(user2, currentUser2.Username);
        Assert.True(currentUser1.IsAuthenticated);
        Assert.True(currentUser2.IsAuthenticated);
    }

    [Fact]
    public void Logout_OneUser_DoesNotAffectOtherUsers()
    {
        // Arrange
        var service = new MockAuthService();
        var user1 = "User1";
        var user2 = "User2";

        var login1 = service.Login(user1);
        var login2 = service.Login(user2);

        // Act
        service.Logout(login1.Token);

        var currentUser1 = service.GetCurrentUser(login1.Token);
        var currentUser2 = service.GetCurrentUser(login2.Token);

        // Assert
        Assert.False(currentUser1.IsAuthenticated);
        Assert.True(currentUser2.IsAuthenticated);
        Assert.Equal(user2, currentUser2.Username);
    }

    [Fact]
    public void Login_WithSpecialCharactersInUsername_WorksCorrectly()
    {
        // Arrange
        var service = new MockAuthService();
        var username = "user@example.com";

        // Act
        var result = service.Login(username);
        var currentUser = service.GetCurrentUser(result.Token);

        // Assert
        Assert.Equal(username, result.Username);
        Assert.NotEmpty(result.Token);
        Assert.True(currentUser.IsAuthenticated);
        Assert.Equal(username, currentUser.Username);
    }

    [Fact]
    public void Login_WithLongUsername_WorksCorrectly()
    {
        // Arrange
        var service = new MockAuthService();
        var username = new string('a', 100);

        // Act
        var result = service.Login(username);
        var currentUser = service.GetCurrentUser(result.Token);

        // Assert
        Assert.Equal(username, result.Username);
        Assert.NotEmpty(result.Token);
        Assert.True(currentUser.IsAuthenticated);
    }
}
