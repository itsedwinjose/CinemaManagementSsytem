using CinemaTicketing.Core.Security;

namespace CinemaTicketing.Tests;

public class PasswordHasherTests
{
    [Fact]
    public void HashPassword_ProducesValueThatCanBeVerified()
    {
        var hasher = new PasswordHasher();

        var hash = hasher.HashPassword("admin123");

        Assert.True(hasher.VerifyPassword("admin123", hash));
    }

    [Fact]
    public void VerifyPassword_ReturnsFalseForWrongPassword()
    {
        var hasher = new PasswordHasher();
        var hash = hasher.HashPassword("admin123");

        Assert.False(hasher.VerifyPassword("wrong-password", hash));
    }
}
