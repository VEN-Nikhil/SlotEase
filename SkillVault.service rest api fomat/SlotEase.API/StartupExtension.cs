using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Reflection;
using SlotEase.Domain;
using SlotEase.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace SlotEase.API;

internal static class StartupExtension
{
    public static string Oktadefaults { get; private set; }

    internal static void InitialiseDatabase(this IServiceCollection services, int retries)
    {
        Polly.Retry.RetryPolicy policy = Policy.Handle<Exception>().
            WaitAndRetry(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    if (retries == retry)
                        throw new InvalidOperationException($" Exception {exception.GetType().Name} with message {exception.Message} detected on attempt {retry} of {retries}", exception);
                }
            );

        policy.Execute(() =>
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            SlotEaseContext dbcotext = serviceProvider.GetService<SlotEaseContext>();
            dbcotext.Database.Migrate();
        });
    }

    internal static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SlotEaseContext>(options =>
        {
            options.UseSqlServer(configuration.Get<ConfigSettings>().SqlServerConnectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(SlotEaseContext).GetTypeInfo().Assembly.GetName().Name);
                });
        }, ServiceLifetime.Scoped);

        return services;
    }

    internal static void ConfigureJWTAuthService(this IServiceCollection services, IConfiguration configuration)
    {
        AuthenticationBuilder authenticationBuilder;
        List<Authorities> authorities = configuration.GetSection(nameof(Authorities))?.Get<List<Authorities>>();
        if (authorities != null)
        {
            authenticationBuilder = services.AddAuthentication();
            foreach (Authorities addAuthority in authorities)
            {
                AddJwtBearerScheme(authenticationBuilder, addAuthority);
            }
        }
        else
        {
            authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            JwtSettings Jwt = configuration.Get<ConfigSettings>()?.Jwt ?? new JwtSettings();

            Authorities authority = new()
            {
                ApiName = Jwt.ApiName,
                ApiSecret = Jwt.ApiSecret,
                AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme,
                RequireHttpsMetadata = Jwt.RequireHttpsMetadata
            };
            AddJwtBearerScheme(authenticationBuilder, authority);
        }
    }
    private static void AddJwtBearerScheme(AuthenticationBuilder authenticationBuilder, Authorities authorities)
    {
        authenticationBuilder.AddJwtBearer(authorities?.AuthenticationScheme ?? JwtBearerDefaults.AuthenticationScheme, options =>
        {
            if (authorities != null)
            {
                options.Authority = authorities.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authorities?.ApiSecret ?? string.Empty)),
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                options.Audience = authorities?.ApiName ?? string.Empty;
                options.RequireHttpsMetadata = authorities?.RequireHttpsMetadata ?? false;
                options.SaveToken = true;
            }
        });

    }
}
