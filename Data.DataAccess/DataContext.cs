using Microsoft.EntityFrameworkCore;
using Data.DataAccess.EntityConfigurationMap;
using Data.DataAccess.Framework;
using Data.Entities;

namespace Data.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {
        }

        public DbSet<Section> Section { get; set; }
        public DbSet<SubSection> SubSection { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new SectionMap());
            modelBuilder.AddConfiguration(new SubSectionMap());
            modelBuilder.AddConfiguration(new ArticleMap());
            modelBuilder.AddConfiguration(new CommentMap());
        }
    }
}
