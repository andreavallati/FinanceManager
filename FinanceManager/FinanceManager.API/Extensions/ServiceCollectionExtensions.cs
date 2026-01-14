using FinanceManager.API.Application.Authorization;
using FinanceManager.API.Application.Authorization.Handlers;
using FinanceManager.API.Application.Interfaces.Authentication;
using FinanceManager.API.Application.Interfaces.Repositories;
using FinanceManager.API.Application.Interfaces.Services;
using FinanceManager.API.Application.Services;
using FinanceManager.API.Application.Validation;
using FinanceManager.API.Domain.Entities;
using FinanceManager.API.Infrastructure.Authentication;
using FinanceManager.API.Infrastructure.Repositories;
using FinanceManager.Shared.Application.Configuration;
using FinanceManager.Shared.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinanceManager.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var clientSecret = jwtSettings.GetValue<string>("ClientSecret")!;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = jwtSettings.GetValue<bool>("ValidateIssuer"),
                                    ValidateAudience = jwtSettings.GetValue<bool>("ValidateAudience"),
                                    ValidateLifetime = jwtSettings.GetValue<bool>("ValidateLifetime"),
                                    ValidateIssuerSigningKey = jwtSettings.GetValue<bool>("ValidateIssuerSigningKey"),
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clientSecret))
                                };
                            });

            builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        }

        public static void ConfigureAuthorization(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.Requirements.Add(new RoleRequirement(nameof(UserRole.Admin))));
                options.AddPolicy("StandardPolicy", policy => policy.Requirements.Add(new RoleRequirement(nameof(UserRole.Standard))));
            });

            builder.Services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
            builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationResultHandler>();
        }

        public static void InjectConfig(this WebApplicationBuilder builder)
        {
            builder.Configuration.SetBasePath(AppContext.BaseDirectory)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                   .AddEnvironmentVariables()
                   .AddEnvironmentVariables("ASPNETCORE_");
        }

        public static void InjectConfigSections(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        }

        public static void InjectValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<User>, UserValidator>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITransactionService, TransactionService>();
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
        }
    }
}