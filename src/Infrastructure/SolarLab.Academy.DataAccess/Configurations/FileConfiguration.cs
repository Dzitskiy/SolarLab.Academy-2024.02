using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SolarLab.Academy.DataAccess.Configurations
{
    /// <summary>
    /// Файл конфигурации сущности файла.
    /// </summary>
    public class FileConfiguration : IEntityTypeConfiguration<Domain.Files.Entity.File>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Domain.Files.Entity.File> builder)
        {
            builder.ToTable("Files").HasKey(t => t.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ContentType).IsRequired().HasMaxLength(255);
        }
    }
}
