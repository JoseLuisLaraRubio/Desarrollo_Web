namespace GymApp.ServiceDefaults.WebAppSettings;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RaptorUtils.AspNet.Applications.Plugins.Serilog;
using RaptorUtils.AspNet.Logging.Filters;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

internal class GymAppSerilogWebAppPlugin(
    Func<WebApplicationBuilder, Task<bool>>? isEnabled = null)
    : SerilogWebAppPlugin(isEnabled)
{
    public override ConsoleTheme? ConsoleTheme => AnsiConsoleTheme.Code;

    protected override void ConfigureLogger(IServiceProvider serviceProvider, LoggerConfiguration options)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        options
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Filter.With(new RequestPathPrefixLogFilter("/api/"))
            .WriteTo.Console(theme: this.ConsoleTheme, applyThemeToRedirectedOutput: true)
            .WriteTo.OpenTelemetry();
    }
}
