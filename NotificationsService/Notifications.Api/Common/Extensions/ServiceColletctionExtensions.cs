using System.Text.Json.Serialization;
using MassTransit;
using Microsoft.OpenApi.Models;
using Notifications.Api.Notifications;
using Notifications.Business.Notifications;
using Notifications.Data.Contexts;
using Notifications.Data.Repositories;

namespace Notifications.Api.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Construct(this IServiceCollection services, ConfigurationManager configuration)
    {
        return services
            .AddApi()
            .AddBusiness(configuration)
            .AddData(configuration);
    }

    private static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notifications API", Version = "v1" });
        });

        return services;
    }

    private static IServiceCollection AddBusiness(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<NotificationService>();
        services.AddMassTransit(x =>
        {
            x.AddConsumer<NotificationsConsumer>();
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], h =>
                {
                    h.Username(configuration["RabbitMQ:Username"]);
                    h.Password(configuration["RabbitMQ:Password"]);
                });
                
                cfg.ReceiveEndpoint("notification-emitted-queue", e =>
                {
                    e.ConfigureConsumer<NotificationsConsumer>(context);
                    e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(2)));
                });
            });
        });
       
        return services;
    }

    private static IServiceCollection AddData(this IServiceCollection services, ConfigurationManager configuration)
    {
        var host = configuration["Postgres:Host"];
        var port = configuration["Postgres:Port"];
        var database = configuration["Postgres:Database"];
        var user = configuration["Postgres:User"];
        var password = configuration["Postgres:Password"];

        var connectionString =
            $"Host={host};Port={port};Database={database};Username={user};Password={password}";

        services.AddNpgsql<NotificationsContext>(connectionString);

        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<INotificationOutboxRepository, NotificationOutboxRepository>();

        return services;
    }
}
