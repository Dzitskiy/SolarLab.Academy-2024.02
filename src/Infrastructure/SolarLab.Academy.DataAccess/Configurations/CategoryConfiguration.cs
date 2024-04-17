using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SolarLab.Academy.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Categories.Entity.Category"/>.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Domain.Categories.Entity.Category>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Domain.Categories.Entity.Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.HasMany(x => x.SubCategories).WithOne(f => f.Parent).HasForeignKey(f => f.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(DataSeedHelper.GetCategories());
        }
    }
}