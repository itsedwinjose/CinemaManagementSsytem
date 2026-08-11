using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Security;
using CinemaTicketing.Core.Services;

namespace CinemaTicketing.Tests;

public class InitialAdminSetupServiceTests
{
    [Fact]
    public async Task CreateInitialAdminAsync_CreatesUserWhenDatabaseIsEmpty()
    {
        var repository = new InMemoryUserRepository();
        var service = new InitialAdminSetupService(repository, new PasswordHasher());

        var result = await service.CreateInitialAdminAsync(new InitialAdminSetupRequest("admin", "Administrator", "admin1234", "admin1234"));

        Assert.True(result.IsSuccess);
        Assert.True(await repository.AnyUsersAsync());
    }

    [Fact]
    public async Task CreateInitialAdminAsync_RejectsShortPassword()
    {
        var repository = new InMemoryUserRepository();
        var service = new InitialAdminSetupService(repository, new PasswordHasher());

        var result = await service.CreateInitialAdminAsync(new InitialAdminSetupRequest("admin", "Administrator", "short", "short"));

        Assert.False(result.IsSuccess);
    }

    private sealed class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public Task<bool> AnyUsersAsync(CancellationToken cancellationToken = default) => Task.FromResult(_users.Count > 0);

        public Task<long> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            _users.Add(new User
            {
                Id = _users.Count + 1,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                PasswordHash = user.PasswordHash,
                IsActive = user.IsActive,
                CreatedUtc = user.CreatedUtc
            });

            return Task.FromResult((long)_users.Count);
        }

        public Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_users.FirstOrDefault(x => string.Equals(x.UserName, userName, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<bool> ValidateCredentialsAsync(string userName, string password, CancellationToken cancellationToken = default)
        {
            var hasher = new PasswordHasher();
            var user = _users.FirstOrDefault(x => string.Equals(x.UserName, userName, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(user is not null && hasher.VerifyPassword(password, user.PasswordHash));
        }

        public Task UpdatePasswordHashAsync(long userId, string passwordHash, CancellationToken cancellationToken = default)
        {
            var u = _users.FirstOrDefault(x => x.Id == userId);
            if (u is not null)
            {
                int index = _users.IndexOf(u);
                _users[index] = new User { Id = u.Id, UserName = u.UserName, DisplayName = u.DisplayName, PasswordHash = passwordHash, IsActive = u.IsActive, CreatedUtc = u.CreatedUtc };
            }
            return Task.CompletedTask;
        }
    }
}
