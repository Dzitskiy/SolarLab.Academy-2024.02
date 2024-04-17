using SolarLab.Academy.Domain.Categories.Entity;

namespace SolarLab.Academy.DataAccess
{
    /// <summary>
    /// Класс для получения данных по-умолчанию.
    /// </summary>
    public static class DataSeedHelper
    {
        /// <summary>
        /// Получить список предустановленных категорий.
        /// </summary>
        /// <returns></returns>
        public static Category[] GetCategories()
        {
            return new[]
            {
                new Category
                {
                    Id = Guid.Parse("10946739-9F9E-4958-81F8-66C52DBA6BEA"),
                    Name = "Недвижимость"
                },
                new Category
                {
                    Id = Guid.Parse("EC698CDE-0525-42C5-8B10-37179ADB3540"),
                    Name = "Дома",
                    ParentId = Guid.Parse("10946739-9F9E-4958-81F8-66C52DBA6BEA")
                },
                new Category
                {
                    Id = Guid.Parse("6737FA84-51B2-4E20-8603-02BE4DCDC9AC"),
                    Name = "Квартиры",
                    ParentId = Guid.Parse("10946739-9F9E-4958-81F8-66C52DBA6BEA")
                },

                new Category
                {
                    Id = Guid.Parse("2E51C519-79C5-4468-A1EE-478CA0C2B8FF"),
                    Name = "Транспорт"
                },
                new Category
                {
                    Id = Guid.Parse("BB805E10-D6C0-4CE5-B8AE-2D2554B516B9"),
                    Name = "Велосипеды",
                    ParentId = Guid.Parse("2E51C519-79C5-4468-A1EE-478CA0C2B8FF")
                },
                new Category
                {
                    Id = Guid.Parse("CB66C03D-FCF8-4E9E-9A37-BFE63C07D5FC"),
                    Name = "Автомобили",
                    ParentId = Guid.Parse("2E51C519-79C5-4468-A1EE-478CA0C2B8FF")
                },

                new Category
                {
                    Id = Guid.Parse("3AAD0E0F-C16E-47E7-A223-B638CC5A48F6"),
                    Name = "Одежда"
                },
                new Category
                {
                    Id = Guid.Parse("9797B747-3432-4F37-8725-D97A3978AB49"),
                    Name = "Одежда для взрослых",
                    ParentId = Guid.Parse("3AAD0E0F-C16E-47E7-A223-B638CC5A48F6")
                },
                new Category
                {
                    Id = Guid.Parse("3B0BEB72-E73B-473D-9804-720E656CB243"),
                    Name = "Детская одежда",
                    ParentId = Guid.Parse("3AAD0E0F-C16E-47E7-A223-B638CC5A48F6")
                }
            };
        }
    }
}