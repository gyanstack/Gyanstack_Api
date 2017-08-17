using Data.DataAccess.Interface;
using Data.Entities;

namespace Data.DataAccess.Repository
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
