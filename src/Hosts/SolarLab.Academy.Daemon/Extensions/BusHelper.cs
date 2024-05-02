using MassTransit;
using System.Reflection;

namespace SolarLab.Academy.Daemon.Extensions
{
    public static class BusHelper
    {
        public static void AddEndpoint<TEvent, TConsumer>(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext context) where TEvent : class where TConsumer : class, IConsumer<TEvent>
        {
            var endpointUrl = $"{Assembly.GetEntryAssembly().GetName().Name}:{typeof(TEvent).FullName}";
            cfg.ReceiveEndpoint(endpointUrl, e =>
            {
                e.Consumer<TConsumer>(context);
                e.UseMessageRetry(configurator =>
                {
                    configurator.Interval(5, TimeSpan.FromSeconds(5));
                });
            });
        }
    }
}
