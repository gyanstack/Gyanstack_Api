using Data.DataAccess.Interface;
using Data.Entities;

namespace Data.DataAccess.Repository
{
    public class ArticleRepository : EntityBaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(DataContext context) : base(context)
        {
        }
    }
}
