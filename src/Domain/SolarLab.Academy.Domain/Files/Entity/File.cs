using SolarLab.Academy.Domain.Base;

namespace SolarLab.Academy.Domain.Files.Entity
{
    /// <summary>
    /// Сущность файла.
    /// </summary>
    public class File : BaseEntity
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Контент файла.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Тип контента.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Размер файла.
        /// </summary>
        public int Length { get; set; }
    }
}
