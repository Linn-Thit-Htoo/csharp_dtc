using csharp_dtc.API.Configurations;
using csharp_dtc.API.OrderDbContextModels;
using csharp_dtc.API.OrderDetailDbContextModels;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace csharp_dtc.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        TransactionManager.ImplicitDistributedTransactions = true;

        builder.Services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.PropertyNamingPolicy = null;
            opt.JsonSerializerOptions.DictionaryKeyPolicy = null;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjectionExtensions).Assembly);
        });

        builder.Services.AddDbContext<OrderDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Dtc1DbConnection"));
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        builder.Services.AddDbContext<OrderDetailDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Dtc2DbConnection"));
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        builder.Services.AddScoped<csharp_dtc.API.OrderPersistence.Wrapper.IUnitOfWork, csharp_dtc.API.OrderPersistence.Wrapper.UnitOfWork>();
        builder.Services.AddScoped<csharp_dtc.API.OrderDetailPersistence.Wrapper.IUnitOfWork, csharp_dtc.API.OrderDetailPersistence.Wrapper.UnitOfWork>();
        builder.Services.AddHealthChecks();
        builder.Services.Configure<AppSetting>(builder.Configuration);

        return services;
    }
}
