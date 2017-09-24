using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
        public string Subsection { get; set; }
        public string Author { get; set; }
        public string ModifiedDate { get; set; }
        public int? SubsectionId { get; set; }
        public string CreatedDate { get; internal set; }
        public string Path { get; internal set; }
        public string UserAvatar { get; internal set; }
        public List<UserComment> UserComments { get; set; }
        public string Section { get; internal set; }
    }
}
