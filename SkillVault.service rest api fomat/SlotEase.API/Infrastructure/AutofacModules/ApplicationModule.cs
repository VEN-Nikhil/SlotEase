using Autofac;
using System.Reflection;
using SlotEase.Domain.Interfaces;
using SlotEase.Infrastructure;
using SlotEase.Infrastructure.Interfaces;
using SlotEase.Infrastructure.Repositories;
using SlotEase.Infrastructure.Services;
using SlotEase.Application.Queries.Setting;

namespace SlotEase.API.Infrastructure.AutofacModules;

/// <summary>
/// 
/// </summary>
public class ApplicationModule : Autofac.Module
{
    /// <summary> 
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();

        // No need to register any more respository since all will be registered using the generic registration below.
        builder.RegisterGeneric(typeof(BaseRepository<>))
            .As(typeof(IRepository<>));


        builder.RegisterType<DapperRepository>()
                    .As<IDapperRepository>()
                    .InstancePerLifetimeScope();

        builder.RegisterType<UserService>()
            .As<IUserService>()
            .InstancePerLifetimeScope();

        builder
            .RegisterAssemblyTypes(typeof(AppConfigurationQueries).GetTypeInfo().Assembly)
            .Where(e => e.Name.EndsWith("Queries"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope(); // no need to register any more Queries services which all end with "Queries" in this assembly
    }
}
