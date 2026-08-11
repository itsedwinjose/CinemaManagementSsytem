using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Core.Services;

public sealed record AuthenticationResult(bool IsSuccess, string Message, User? User);

public interface IAuthenticationService
{
    Task<bool> HasAnyUsersAsync(CancellationToken cancellationToken = default);
    Task<AuthenticationResult> LoginAsync(string userName, string password, CancellationToken cancellationToken = default);
}

public sealed class AuthenticationService(IUserRepository userRepository) : IAuthenticationService
{
    public Task<bool> HasAnyUsersAsync(CancellationToken cancellationToken = default)
    {
        return userRepository.AnyUsersAsync(cancellationToken);
    }

    public async Task<AuthenticationResult> LoginAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
        {
            return new AuthenticationResult(false, "Enter both username and password.", null);
        }

        var user = await userRepository.GetByUserNameAsync(userName.Trim(), cancellationToken);
        if (user is null || !user.IsActive)
        {
            return new AuthenticationResult(false, "Invalid username or password.", null);
        }

        var isValid = await userRepository.ValidateCredentialsAsync(userName.Trim(), password, cancellationToken);
        return isValid
            ? new AuthenticationResult(true, "Login successful.", user)
            : new AuthenticationResult(false, "Invalid username or password.", null);
    }
}
