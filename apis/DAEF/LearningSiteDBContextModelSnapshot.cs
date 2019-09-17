using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DACommon;
using DACommon.Entities;
using System.Linq;
using System.Reflection;

namespace DAEF
{
    [DbContext(typeof(LearningSiteDBContext))]
    public class LearningSiteDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            var entities = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !t.IsAbstract && typeof(BaseEntity).IsAssignableFrom(t));

            foreach (var item in entities)
            {
                modelBuilder.Entity(item.FullName, b =>
               {

                   foreach (var prop in item.GetProperties())
                   {
                       if (prop.Name == "Id")
                       {
                           b.Property(prop.PropertyType, prop.Name)
                               .IsRequired()
                                .UseSqlServerIdentityColumn();

                           b.HasKey(prop.Name);
                       }
                       else
                       {
                           var attrs = prop.GetCustomAttributes();

                           bool hasRequired = attrs.Any(c => c.GetType() == typeof(Required));
                           bool longText = attrs.Any(c => c.GetType() == typeof(LongText));
                           MaxLength maxLength = attrs.FirstOrDefault(c => c.GetType() == typeof(MaxLength)) as MaxLength;
                           bool isConcurrncyToken = attrs.Any(c => c.GetType() == typeof(ConcurrencyToken));


                           if (attrs.Any(a => a.GetType() == typeof(ReferenceEntity)))
                           {
                               ReferenceEntity refAtt = attrs.First(a => a.GetType() == typeof(ReferenceEntity)) as ReferenceEntity;

                               b.HasIndex(refAtt.Name);

                               b.HasOne(prop.PropertyType.FullName)
                                   .WithMany()
                                   .HasForeignKey(refAtt.Name);
                           }
                           else
                           {
                               b.Property(prop.PropertyType, prop.Name)
                                  .IsRequired(hasRequired)
                                  .IsConcurrencyToken(isConcurrncyToken)
                                  .HasMaxLength(longText ? 5000 : (maxLength != null ? maxLength.Count : 300));
                           }
                       }

                   }

                   b.ToTable("tbl" + item.Name);

               });

            }

        }
    }
}
