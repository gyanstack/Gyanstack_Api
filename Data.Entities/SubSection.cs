using System.Collections.Generic;

namespace Data.Entities
{
    public class SubSection : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
        public int SectionId { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
