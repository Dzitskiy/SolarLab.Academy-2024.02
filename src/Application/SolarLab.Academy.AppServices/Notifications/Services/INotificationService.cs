namespace SolarLab.Academy.AppServices.Notifications.Services
{
    /// <summary>
    /// Сервис для работы с уведомлениями.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Отправка уведомления о создании пользователя.
        /// </summary>
        /// <param name="id">Идентификатор созданного пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task SendUserWasCreatedAsync(Guid id, CancellationToken cancellationToken);
    }
}
