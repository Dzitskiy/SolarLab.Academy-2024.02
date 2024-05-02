using MassTransit;
using SolarLab.Academy.Contracts.Events;

namespace SolarLab.Academy.AppServices.Notifications.Services
{
    /// <inheritdoc cref="INotificationService"/>
    public class NotificationService : INotificationService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public NotificationService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        /// <inheritdoc/>
        public async Task SendUserWasCreatedAsync(Guid id, CancellationToken cancellationToken)
        {
            var message = new UserCreatedEvent(id);
            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
