using Data.DataAccess.Framework;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DataAccess.EntityConfigurationMap
{
    public class SubSectionMap : EntityTypeConfiguration<SubSection>
    {
        public override void Map(EntityTypeBuilder<SubSection> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Section)
                .WithMany(x => x.Childs)
                .HasForeignKey(x => x.SectionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("SubSection");
        }
    }
}
