using Data.DataAccess.Interface;
using Data.Entities;

namespace Data.DataAccess.Repository
{
    public class SubSectionRepository : EntityBaseRepository<SubSection>, ISubSectionRepository
    {
        public SubSectionRepository(DataContext context) : base(context)
        {
        }
    }
}
