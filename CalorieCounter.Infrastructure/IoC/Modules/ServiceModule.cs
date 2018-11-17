using Autofac;
using CalorieCounter.Core.Domain;
using CalorieCounter.Infrastructure.Commands;
using CalorieCounter.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CalorieCounter.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                            .GetTypeInfo()
                            .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x=>x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();


            builder.RegisterType<PasswordHasher<User>>()
                        .As<IPasswordHasher<User>>();

            builder.RegisterType<JwtHandler>()
                        .As<IJwtHandler>()
                        .SingleInstance();

        }
    }
}