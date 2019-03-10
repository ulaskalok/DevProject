using AutoMapper;
using Dev.Application;
using Dev.Application.Infrastructure;
using Dev.Application.Mappers;
using Dev.Application.Services;
using Dev.Data.Context;
using Dev.Data.Extensions;
using Dev.Data.Interface;
using Dev.Data.Settings;
using Dev.Domain;
using Dev.Domain.Interfaces;
using Dev.Infrastructure.Repository;
using Dev.Infrastructure.UnitOfWork;
using Dev.Instratructure.Authentication;
using Dev.Instratructure.Cache;
using Dev.Instratructure.ElasticSearch;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Reflection;

namespace Dev.Instratructure.IOC
{
    public sealed class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            RegisterApplication(services);
            RegisterMediatr(services);
        }
        public static void RegisterMediatr(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddMediatR(typeof(Application.Commands.Page.CreatePageCommandHandler).GetTypeInfo().Assembly);
        }
        public static void RegisterApplication(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IContextFactory), typeof(ContextFactory));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IElasticSearch), typeof(ElasticSearch.ElasticSearch));
            services.AddTransient(typeof(IBaseService<,>), typeof(BaseService<,>));
            services.AddTransient(typeof(IAccountService), typeof(AccountService));
            services.AddTransient(typeof(ICache), typeof(RedisCache));
            services.AddTransient(typeof(ILogger), typeof(Logger.Logger));

            AutoMapperInitializer.Initialize();
            services.AddSingleton(typeof(IMapper), provider => Mapper.Instance);
        }

        public static void RegisterAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("AuthSettings").Get<AuthSettings>();
            services.AddTransient(typeof(IJwt), typeof(JwtService));
            services.AddTransient(typeof(IAuthorize), typeof(Authorize));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = config.Issuer,
            ValidateAudience = true,
            ValidAudience = config.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = config.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
            ClockSkew = System.TimeSpan.Zero
        };
    });
        }

        public static void RegisterEF(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Dev.Data.Settings.Data>(c =>
            {
                c.Provider = (Data.Settings.DataProvider)Enum.Parse(
                    typeof(Data.Settings.DataProvider),
                    configuration.GetSection("Data")["Provider"]);
            });

            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<AuthSettings>(configuration);
            services.Configure<AuthSettings>(options => configuration.GetSection("AuthSettings").Bind(options));

            services.AddEntityFramework(configuration);
        }
    }
}
