namespace GymApp.ServiceDefaults.WebAppSettings;

using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RaptorUtils.AspNet.Applications;
using RaptorUtils.AspNet.Applications.Plugins.Swagger;
using RaptorUtils.Extensions.Configuration;

using SwaggerThemes;

public abstract class GymAppWebAppDefinition : PluginEnabledWebAppDefinition
{
    protected override ICollection<WebAppPlugin>? GetPlugins()
    {
        return [new GymAppSerilogWebAppPlugin(), GetSwaggerPlugin()];
    }

    protected override async Task ConfigureServices(WebApplicationBuilder builder)
    {
        await base.ConfigureServices(builder);

        builder.AddServiceDefaults();
    }

    protected override async Task Configure(WebApplication app)
    {
        await base.Configure(app);

        app.MapDefaultEndpoints();
    }

    private static SwaggerWebAppPlugin GetSwaggerPlugin()
    {
        return new SwaggerWebAppPlugin(
            isEnabled: IsSwaggerEnabled,
            metadataAction: (builder) => builder.Services.AddEndpointsApiExplorer(),
            setupActionProvider: (_) => (options) =>
            {
                options.SupportNonNullableReferenceTypes();
                options.NonNullableReferenceTypesAsRequired();
            },
            themeProvider: (_) => Theme.Vs2022);
    }

    private static Task<bool> IsSwaggerEnabled(WebApplicationBuilder builder)
    {
        return Task.FromResult(IsSwaggerEnabled(builder.Configuration, builder.Environment));
    }

    private static bool IsSwaggerEnabled(IConfiguration configuration, IWebHostEnvironment environment)
    {
        return configuration.IsEnabled("ENABLE_SWAGGER", environment.IsDevelopment());
    }
}
