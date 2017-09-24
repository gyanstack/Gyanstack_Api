using Data.DataAccess.Framework;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DataAccess.EntityConfigurationMap
{
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public override void Map(
            EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.SubSection)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.SubSectionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.User)
               .WithMany(x => x.Articles)
               .HasForeignKey(x => x.AuthorId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("Article");
        }
    }
}
