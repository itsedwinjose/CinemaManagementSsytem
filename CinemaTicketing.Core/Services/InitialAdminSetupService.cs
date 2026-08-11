using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Security;

namespace CinemaTicketing.Core.Services;

public sealed record InitialAdminSetupRequest(string UserName, string DisplayName, string Password, string ConfirmPassword);
public sealed record InitialAdminSetupResult(bool IsSuccess, string Message);

public interface IInitialAdminSetupService
{
    Task<InitialAdminSetupResult> CreateInitialAdminAsync(InitialAdminSetupRequest request, CancellationToken cancellationToken = default);
}

public sealed class InitialAdminSetupService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IInitialAdminSetupService
{
    public async Task<InitialAdminSetupResult> CreateInitialAdminAsync(InitialAdminSetupRequest request, CancellationToken cancellationToken = default)
    {
        if (await userRepository.AnyUsersAsync(cancellationToken))
        {
            return new InitialAdminSetupResult(false, "Initial setup is already complete.");
        }

        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.DisplayName))
        {
            return new InitialAdminSetupResult(false, "Enter both username and display name.");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return new InitialAdminSetupResult(false, "Enter a password.");
        }

        if (request.Password.Length < 8)
        {
            return new InitialAdminSetupResult(false, "Password must be at least 8 characters.");
        }

        if (!string.Equals(request.Password, request.ConfirmPassword, StringComparison.Ordinal))
        {
            return new InitialAdminSetupResult(false, "Passwords do not match.");
        }

        var existingUser = await userRepository.GetByUserNameAsync(request.UserName.Trim(), cancellationToken);
        if (existingUser is not null)
        {
            return new InitialAdminSetupResult(false, "That username already exists.");
        }

        var user = new User
        {
            UserName = request.UserName.Trim(),
            DisplayName = request.DisplayName.Trim(),
            PasswordHash = passwordHasher.HashPassword(request.Password),
            IsActive = true,
            CreatedUtc = DateTime.UtcNow
        };

        await userRepository.CreateAsync(user, cancellationToken);
        return new InitialAdminSetupResult(true, "Administrator account created successfully.");
    }
}
