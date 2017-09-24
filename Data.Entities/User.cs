using System.Collections.Generic;

namespace Data.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public int UserType { get; set; }
        public string UserAvatar { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}