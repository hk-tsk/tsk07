using DACommon.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAEFC.EntityConfigurations
{
    public class CoursesEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.ToTable("tblCategory");

            builder.Property(typeof(long), "Id")
                .IsRequired();

            builder.Property(typeof(string), "ConcurrencyStamp")
            .IsRequired(true)
            .HasMaxLength(300)
            .IsConcurrencyToken(true);

            builder.Property(typeof(string), "Title")
            .IsRequired(true)
            .HasMaxLength(300);

            builder.Property(typeof(string), "Name")
              .IsRequired(true)
              .IsConcurrencyToken(false).
              HasMaxLength(300);

            builder.HasOne(x=>x.Course)
               .WithMany(x=>x.Categories)
              .HasForeignKey("CourseId")
              .IsRequired();

        }
    }
}
