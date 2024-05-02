using MassTransit;
using SolarLab.Academy.Contracts.Events;

namespace SolarLab.Academy.Daemon.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            Console.WriteLine($"Идентификатор созданного пользователя {context.Message.Id}");
        }

    }
}
