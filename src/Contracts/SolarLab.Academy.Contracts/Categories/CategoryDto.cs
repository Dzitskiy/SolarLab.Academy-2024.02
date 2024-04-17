namespace SolarLab.Academy.Contracts.Categories
{
    /// <summary>
    /// Информация о категории.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}