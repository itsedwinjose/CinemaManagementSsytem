using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface IUserRepository
{
    Task<bool> AnyUsersAsync(CancellationToken cancellationToken = default);
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<long> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> ValidateCredentialsAsync(string userName, string password, CancellationToken cancellationToken = default);
    Task UpdatePasswordHashAsync(long userId, string passwordHash, CancellationToken cancellationToken = default);
}
