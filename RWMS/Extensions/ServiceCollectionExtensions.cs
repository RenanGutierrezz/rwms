using RWMS.Services;
using RWMS.Services.Interfaces;

namespace RWMS.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all application-level services (business logic layer).
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Email — transient: stateless, no shared state needed between injections
        services.AddTransient<IEmailService, EmailService>();

        // Business logic services — scoped: one instance per HTTP request,
        // aligned with the DbContext lifetime to avoid context threading issues
        // services.AddScoped<IProductService, ProductService>();
        // services.AddScoped<IOrderService, OrderService>();
        // services.AddScoped<ISupplyService, SupplyService>();
        // services.AddScoped<IFinanceService, FinanceService>();

        return services;
    }
}
