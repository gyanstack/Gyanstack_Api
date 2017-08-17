using Data.DataAccess.Interface;
using Data.Entities;

namespace Data.DataAccess.Repository
{
    public class CommentRepository : EntityBaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }
    }
}
