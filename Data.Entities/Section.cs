using System.Collections.Generic;

namespace Data.Entities
{
    public class Section : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<SubSection> Childs { get; set; }
    }
}
