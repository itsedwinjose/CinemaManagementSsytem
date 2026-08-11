using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Security;
using CinemaTicketing.Core.Services;
using CinemaTicketing.Data.Database;
using CinemaTicketing.Data.Repositories;
using CinemaTicketing.WinForms.Configuration;
using CinemaTicketing.WinForms.Printing;

namespace CinemaTicketing.WinForms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        try
        {
            var settings = ConfigurationLoader.Load(AppContext.BaseDirectory);
            var connectionFactory = new MySqlConnectionFactory(settings.Database);
            var databaseBootstrapper = new DatabaseBootstrapper(connectionFactory);
            var databaseHealthCheckService = new DatabaseHealthCheckService(connectionFactory);
            var passwordHasher = new PasswordHasher();
            var userRepository = new UserRepository(connectionFactory, passwordHasher);
            var authenticationService = new AuthenticationService(userRepository);
            var initialAdminSetupService = new InitialAdminSetupService(userRepository, passwordHasher);

            var cinemaRepository = new CinemaRepository(connectionFactory);
            var showTypeRepository = new ShowTypeRepository(connectionFactory);
            var theatreSettingRepository = new TheatreSettingRepository(connectionFactory);
            var applicationSettingRepository = new ApplicationSettingRepository(connectionFactory);
            var theatreSettingsService = new TheatreSettingsService(cinemaRepository, showTypeRepository, theatreSettingRepository, applicationSettingRepository);

            var audiRepository = new AudiRepository(connectionFactory);
            var seatClassRepository = new SeatClassRepository(connectionFactory);
            var audiLayoutRepository = new AudiLayoutRepository(connectionFactory);
            var movieRepository = new MovieRepository(connectionFactory);
            var screeningRepository = new ScreeningRepository(connectionFactory);
            var bookingRepository = new BookingRepository(connectionFactory);
            var cashoutRepository = new CashoutRepository(connectionFactory);

            var layoutService = new AudiLayoutService(audiRepository, seatClassRepository, audiLayoutRepository);
            var movieService = new MovieService(movieRepository);
            var bookingService = new BookingService(bookingRepository, applicationSettingRepository);
            var cashoutService = new CashoutService(cashoutRepository, applicationSettingRepository);
            var printService = new ThermalPrintService();

            databaseBootstrapper.InitializeAsync().GetAwaiter().GetResult();

            if (!authenticationService.HasAnyUsersAsync().GetAwaiter().GetResult())
            {
                using var setupForm = new InitialAdminSetupForm(initialAdminSetupService);
                if (setupForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            using var loginForm = new LoginForm(authenticationService);
            if (loginForm.ShowDialog() != DialogResult.OK || loginForm.AuthenticatedUser is null)
            {
                return;
            }

            Application.Run(new MainForm(
                screeningRepository,
                audiRepository,
                audiLayoutRepository,
                bookingService,
                cashoutService,
                movieService,
                layoutService,
                theatreSettingsService,
                cinemaRepository,
                seatClassRepository,
                userRepository,
                passwordHasher,
                printService,
                loginForm.AuthenticatedUser
            ));
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Application startup failed.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                "Startup Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
