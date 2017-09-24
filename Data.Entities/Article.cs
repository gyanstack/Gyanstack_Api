using System.Collections.Generic;

namespace Data.Entities
{
    public class Article : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string Route { get; set; }
        public int? AuthorId { get; set; }
        public int? SubSectionId { get; set; }
        public int UserView { get; set; }

        public virtual SubSection SubSection { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
