namespace SolarLab.Academy.Contracts.Events
{
    /// <summary>
    /// Событие о создании пользователя.
    /// </summary>
    public class UserCreatedEvent
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Инициализация экземпляра <see cref="UserCreatedEvent"/>.
        /// </summary>
        public UserCreatedEvent(Guid id)
        {
            Id = id;
        }
    }
}
