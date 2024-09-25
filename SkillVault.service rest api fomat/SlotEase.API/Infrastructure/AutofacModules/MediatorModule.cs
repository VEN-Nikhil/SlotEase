using System;
using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using SlotEase.Application.Commands;

namespace SlotEase.API.Infrastructure.AutofacModules;

/// <summary>
/// 
/// </summary>
public class MediatorModule : Autofac.Module
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        Assembly applicationAssembly = typeof(CreateBrokerCommand).GetTypeInfo().Assembly;


        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                  .AsImplementedInterfaces();

        Type[] mediatrOpenTypes =
        [
            typeof(IRequestHandler<,>),
            typeof(INotificationHandler<>),
            typeof(IValidator<>)
        ];

        foreach (Type mediatrOpenType in mediatrOpenTypes)
        {
            builder
                .RegisterAssemblyTypes(applicationAssembly)
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();

        }
    }
}
