using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using SlotEase.API.Serilog;
using SlotEase.Infrastructure;
using ILogger = Serilog.ILogger;

namespace SlotEase.API;

/// <summary>
/// 
/// </summary>
public static class Program
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly string Namespace = typeof(Program).Namespace;
    /// <summary>
    /// 
    /// </summary>
    public static readonly string AppName = Namespace[(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1)..];
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {

        IConfiguration configuration = GetConfiguration();
        Log.Logger = CreateSerilogLogger(configuration);

        try
        {
            Log.Information("Configuring web host ({ApplicationContext})...", AppName);
            IHostBuilder host = CreateHostBuilder(args, configuration);
            IHost test = host.Build();


            using (IServiceScope scope = test.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                try
                {
                    SlotEaseContext context = services.GetService<SlotEaseContext>();
                    ILogger<DbInitializer> iLogger = services.GetService<ILogger<DbInitializer>>();
                    IHostEnvironment environenment = services.GetService<IHostEnvironment>();
                    new DbInitializer(iLogger, environenment, context).SeedAsync().Wait();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, ex.Message);
                }
            }
            Log.Information("Starting web host ({ApplicationContext})...", AppName);
            test.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);

        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
    {
        return Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .UseSerilog((builderContext, config) =>
        {
            ConfigureLogger(config, configuration);
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {

            webBuilder.ConfigureLogging((hostingContext, loggingbuilder) =>
            {
                loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                loggingbuilder.AddConsole();
                loggingbuilder.AddDebug();
            });
            webBuilder.UseStartup<Startup>();

        });

    }

    private static IConfiguration GetConfiguration()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }

    private static ILogger CreateSerilogLogger(IConfiguration configuration)
    {
        LoggerConfiguration loggerConfig = new();
        return ConfigureLogger(loggerConfig, configuration).CreateLogger();
    }

    private static LoggerConfiguration ConfigureLogger(LoggerConfiguration config, IConfiguration configuration)
    {
        string logFilePath = configuration["LogFilePath"];

        LoggerConfiguration loggerConfig = config
                   .MinimumLevel.Verbose()
                   .Enrich.WithProperty("ApplicationContext", AppName)
                   .Enrich.FromLogContext()
                   .Enrich.WithMachineName()
                   .Enrich.WithOperationId()
                   .WriteTo.Console()
                   .WriteTo.File(string.IsNullOrWhiteSpace(logFilePath) ? "Logs.txt" : logFilePath);

        return loggerConfig;
    }
}
