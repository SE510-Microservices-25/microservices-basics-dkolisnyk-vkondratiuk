using System.Text.Json.Serialization;
using Subscriptions.Business.Notifications;
using Subscriptions.Business.Subscriptions;
using Subscriptions.Data.Contexts;
using Subscriptions.Data.Repositories;

namespace Subscriptions.Api.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Construct(this IServiceCollection services, ConfigurationManager configuration)
    {
        return services
            .AddApi(configuration)
            .AddBusiness(configuration)
            .AddData(configuration);
    }

    private static IServiceCollection AddApi(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        return services;
    }

    private static IServiceCollection AddBusiness(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<SubscriptionsService>();

        // remote calls to notification service
        services.AddScoped<HttpClient>();
        services.AddScoped<NotificationHttpRemoteServiceConfiguration>(sp =>
            new NotificationHttpRemoteServiceConfiguration { Host = configuration["Remote:NotificationService:Host"] });
        services.AddScoped<INotificationRemoteService, NotificationHttpRemoteService>();

        return services;
    }

    private static IServiceCollection AddData(this IServiceCollection services, ConfigurationManager configuration)
    {
        var postgresHost = configuration["Postgres:Host"];
        var postgresPort = configuration["Postgres:Port"];
        var postgresDatabase = configuration["Postgres:Database"];
        var postgresUser = configuration["Postgres:User"];
        var postgresPassword = configuration["Postgres:Password"];

        var connectionString =
            $"Host={postgresHost};Port={postgresPort};Database={postgresDatabase};Username={postgresUser};Password={postgresPassword}";

        services.AddNpgsql<ApplicationContext>(connectionString);

        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        return services;
    }
}
