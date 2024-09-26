using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SlotEase.API.Common;
using SlotEase.API.Constants;
using SlotEase.API.Infrastructure.AutofacModules;
using SlotEase.Application.Mapping;
using SlotEase.Domain;
using System.IO;
using System.Reflection;

namespace SlotEase.API;

/// <summary>
/// 
/// </summary>
public class Startup
{
    private const string CorsPolicy = "CorsPolicy";
    private readonly IHostEnvironment _hostingEnvironment;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="hostingEnvironment"></param>
    public Startup(IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        Configuration = configuration;
        _hostingEnvironment = hostingEnvironment;
    }

    /// <summary>
    /// 
    /// </summary>
    public IConfiguration Configuration { get; }
    /// <summary>
    /// 
    /// </summary>
    public ILifetimeScope AutofacContainer { get; private set; }


    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });

        services.Configure<ConfigSettings>(Configuration);
        services.Configure<FormOptions>(x =>
        {
            x.ValueLengthLimit = int.MaxValue;
            x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
        });
        services.AddMvcCore()
            .AddApiExplorer()
            .AddDataAnnotations()
            .AddFormatterMappings();
        services.AddHttpContextAccessor();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(GeneralConstants.ApiVersion, new OpenApiInfo
            {
                Title = GeneralConstants.AppName,
                Version = GeneralConstants.ApiVersion,
            });
            c.AddSecurityDefinition(GeneralConstants.SecurityDefinition, new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = GeneralConstants.SecurityDefinition,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = GeneralConstants.SecurityDefinitionDescription,
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = GeneralConstants.SecurityDefinition
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                                              $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicy,
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .WithOrigins([Configuration.Get<ConfigSettings>().CorsOrigin])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        services.AddAuthorization();
        services.ConfigureJWTAuthService(Configuration);
        services.AddAutoMapper(typeof(AutoMapping));
        services.AddCustomDbContext(Configuration);
        services.InitialiseDatabase(3);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>

    public static void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new MediatorModule());
        builder.RegisterModule(new ApplicationModule());
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error");
        }
        AutofacContainer = app.ApplicationServices.GetAutofacRoot();
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor |
            ForwardedHeaders.XForwardedProto
        });
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCors(CorsPolicy);
        app.UseResponseCaching();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Data Maintenance");
        });
    }
}
