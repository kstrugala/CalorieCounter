using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CalorieCounter.Infrastructure.IoC;
using CalorieCounter.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using CalorieCounter.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using CalorieCounter.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using GraphQL;
using CalorieCounter.Infrastructure.GraphQL;
using GraphQL.Types;
using CalorieCounter.Infrastructure.GraphQL.Queries;
using CalorieCounter.Infrastructure.GraphQL.Types;

namespace CalorieCounter.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            
            services.AddOptions();
            services.AddMvc();

            services.AddAuthorization(options => {
                options.AddPolicy("Admin", p => p.RequireRole("admin"));
                options.AddPolicy("User", p => p.RequireRole("user"));
            });

            services.AddAuthentication(o=>{
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg=>{
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });            

            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Calorie Counter API", Version = "v1" });
            });


            services.AddDbContext<CalorieCounterContext>(options => 
                        options.UseSqlServer(Configuration.GetConnectionString("CalorieCounterDb"), b => b.MigrationsAssembly("CalorieCounter.Api"))
                    );

            services.AddTransient<TokenManagerMiddleware>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedRedisCache(r => {
                r.Configuration = Configuration["redis:ConnectionString"];
            }); 

            services.AddScoped<GraphQLQuery>();
            services.AddScoped<ProductType>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));

            builder.RegisterType<DocumentExecuter>().As<IDocumentExecuter>();
            builder.RegisterType<GraphQLSchema>().As<ISchema>();
            builder.Register<Func<Type, GraphType>>(c => 
            {
                var context = c.Resolve<IComponentContext>();
                return t => {
                    var res = context.Resolve(t);
                    return (GraphType)res;
                };
            });

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();




            app.UseAuthentication();
            app.UseMiddleware<TokenManagerMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calorie Counter API V1");
            });


            app.UseMvc();
        }
    }
}
