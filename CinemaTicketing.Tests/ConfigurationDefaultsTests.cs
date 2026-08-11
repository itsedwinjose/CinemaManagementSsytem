namespace CinemaTicketing.Tests;

public class ConfigurationDefaultsTests
{
    [Fact]
    public void DatabaseSettings_DefaultPort_IsMySqlDefault()
    {
        var settings = new CinemaTicketing.Core.Configuration.DatabaseSettings();

        Assert.Equal((uint)3306, settings.Port);
    }
}
