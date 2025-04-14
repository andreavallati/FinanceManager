using FinanceManager.WPF.Application.Interfaces.Connectors;
using FinanceManager.WPF.Application.Interfaces.Services.Factory;
using FinanceManager.WPF.Application.Services;
using FinanceManager.WPF.Application.Services.Factory;
using FinanceManager.WPF.Application.Validation;
using FinanceManager.WPF.Infrastructure.Connectors;
using FinanceManager.WPF.Presentation.Interfaces;
using FinanceManager.WPF.Presentation.ViewModels;
using FinanceManager.WPF.Presentation.Views;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.WPF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void InjectRest(this ServiceCollection services)
        {
            services.AddSingleton<IRestManager, RestManager>();
            services.AddSingleton<IRestConnector, RestConnector>();
        }

        public static void InjectValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<LoginViewModel>, LoginValidator>();
            services.AddScoped<IValidator<RegisterViewModel>, UserValidator>();
        }

        public static void InjectUIServices(this ServiceCollection services)
        {
            services.AddSingleton<IFactoryUIService, FactoryUIService>();
            services.AddScoped<AuthUIService>();
            services.AddScoped<UserUIService>();
        }

        public static void InjectViewModels(this ServiceCollection services)
        {
            services.AddScoped<ILoginViewModel, LoginViewModel>();
            services.AddScoped<IRegisterViewModel, RegisterViewModel>();
        }

        public static void InjectViews(this ServiceCollection services)
        {
            services.AddScoped<LoginView>();
            services.AddTransient<RegisterView>();
        }
    }
}