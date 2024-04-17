namespace SolarLab.Academy.Domain.Categories.Entity
{
    /// <summary>
    /// Категория.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор родительской категории.
        /// Если null, то корневая категория.
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Родительская категория.
        /// </summary>
        public Category? Parent { get; set; }

        /// <summary>
        /// Список подкатегорий.
        /// </summary>
        public ICollection<Category> SubCategories { get; set; } = Array.Empty<Category>();
    }
}