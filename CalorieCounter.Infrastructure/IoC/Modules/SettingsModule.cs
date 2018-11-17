using Autofac;
using CalorieCounter.Infrastructure.Extensions;
using CalorieCounter.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace CalorieCounter.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
             builder.RegisterInstance(_configuration.GetSettings<JwtSettings>())
                .SingleInstance();
        }
    }
}