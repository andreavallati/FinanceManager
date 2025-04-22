using FinanceManager.WPF.Application.Interfaces.Connectors;
using FinanceManager.WPF.Application.Interfaces.Services.Factory;
using FinanceManager.WPF.Application.Services;
using FinanceManager.WPF.Application.Services.Factory;
using FinanceManager.WPF.Application.Validation;
using FinanceManager.WPF.Infrastructure.Connectors;
using FinanceManager.WPF.Presentation.Interfaces.Factory;
using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using FinanceManager.WPF.Presentation.ViewModels;
using FinanceManager.WPF.Presentation.Views;
using FinanceManager.WPF.Presentation.Views.Factory;
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
            services.AddTransient<IValidator<LoginViewModel>, LoginValidator>();
            services.AddTransient<IValidator<RegisterViewModel>, UserValidator>();
            services.AddTransient<IValidator<AdminViewModel>, AdminValidator>();
            services.AddTransient<IValidator<StandardViewModel>, StandardValidator>();
        }

        public static void InjectUIServices(this ServiceCollection services)
        {
            services.AddSingleton<IFactoryUIService, FactoryUIService>();
            services.AddTransient<AuthUIService>();
            services.AddTransient<UserUIService>();
            services.AddTransient<TransactionUIService>();
        }

        public static void InjectViewModels(this ServiceCollection services)
        {
            services.AddTransient<ILoginViewModel, LoginViewModel>();
            services.AddTransient<IRegisterViewModel, RegisterViewModel>();
            services.AddTransient<IDashboardViewModel, DashboardViewModel>();
            services.AddTransient<IAdminViewModel, AdminViewModel>();
            services.AddTransient<IStandardViewModel, StandardViewModel>();
        }

        public static void InjectViews(this ServiceCollection services)
        {
            services.AddSingleton<IViewFactory, ViewFactory>();
            services.AddSingleton<LoginView>();
            services.AddTransient<RegisterView>();
            services.AddTransient<DashboardView>();
            services.AddTransient<AdminView>();
            services.AddTransient<StandardView>();
        }
    }
}