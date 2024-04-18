using SolarLab.Academy.Contracts.Files;

namespace SolarLab.Academy.AppServices.Files.Services
{
    /// <summary>
    /// Сервис для работы с файлами.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Получение информации о файле по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о файле.</returns>
        Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление файла по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="model">Модель файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор загруженного файла.</returns>
        Task<Guid> UploadAsync(FileDto model, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информаци о скачиваемом файле.</returns>
        Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }
}
