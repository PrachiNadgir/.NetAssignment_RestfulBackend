using Serilog;

namespace Infrastructure.Logging;

public static class SerilogConfiguration
{
    public static void ConfigureSerilog()
    {
        Log.Logger =
            new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(
                "Logs/log-.txt",
                rollingInterval:
                RollingInterval.Day)
            .CreateLogger();
    }
}