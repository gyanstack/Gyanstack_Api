using System.Collections.Generic;

namespace Data.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}