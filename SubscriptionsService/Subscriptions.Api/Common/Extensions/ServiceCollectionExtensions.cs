using System.Text.Json.Serialization;
using MassTransit;
using Subscriptions.Business.Common.MessageBroker;
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
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], h =>
                {
                    h.Username(configuration["RabbitMQ:Username"]);
                    h.Password(configuration["RabbitMQ:Password"]);
                });
            });
        });
        services.AddScoped<IMessageBroker, RabbitMqMessageBroker>();
        services.AddScoped<INotificationProducer, MockNotificationProducer>();
        services.AddScoped<BackgroundNotificationProducer>();
        
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
        services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();

        return services;
    }
}
