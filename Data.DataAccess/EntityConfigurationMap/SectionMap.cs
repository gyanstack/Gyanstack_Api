using Data.DataAccess.Framework;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DataAccess.EntityConfigurationMap
{
    public class SectionMap : EntityTypeConfiguration<Section>
    {
        public override void Map(EntityTypeBuilder<Section> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Section");
        }
    }
}
