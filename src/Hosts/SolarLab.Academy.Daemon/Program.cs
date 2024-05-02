using MassTransit;
using SolarLab.Academy.Contracts.Events;
using SolarLab.Academy.Daemon.Consumers;
using SolarLab.Academy.Daemon.Extensions;

namespace SolarLab.Academy.Daemon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddMassTransit(bus =>
            {
                bus.UsingRabbitMq((ctx, configurator) =>
                {
                    configurator.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                    configurator.AddEndpoint<UserCreatedEvent, UserCreatedConsumer>(ctx);
                });
                bus.AddConsumer<UserCreatedConsumer>();
            });
            builder.Services.AddMassTransitHostedService();

            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}