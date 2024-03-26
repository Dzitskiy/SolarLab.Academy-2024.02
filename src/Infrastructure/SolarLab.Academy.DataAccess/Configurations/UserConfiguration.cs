using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Users.Entity.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Users.Entity.User> builder)
        {
            builder
                .ToTable("Users")
                .HasKey(t => t.Id);

            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.MiddleName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.BirthDate)
                .IsRequired();
        }
    }

}
