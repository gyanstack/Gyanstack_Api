using Data.DataAccess.Interface;
using Data.Entities;

namespace Data.DataAccess.Repository
{
    public class SectionRepository : EntityBaseRepository<Section>, ISectionRepository
    {
        public SectionRepository(DataContext context) : base(context)
        {
        }
    }
}
