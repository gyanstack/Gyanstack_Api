using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class ArticleViewModel : BaseViewModel
    {
        public ArticleViewModel()
        {
            UserList = new List<DropdownViewModel>();
            SectionList = new List<DropdownViewModel>();
            SubSectionList = new List<DropdownViewModel>();
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Path { get; set; }
        [Required]
        public string Route { get; set; }
        public string Author { get; set; }
        public string Section { get; set; }
        public string SubSection { get; set; }
        public int? SubSectionId { get; set; }
        public int? SectionId { get; set; }
        public int? AuthorId { get; set; }
        public IFormFile File { get; set; }
        public List<string> ImageFileName { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<DropdownViewModel> UserList { get; set; }
        public List<DropdownViewModel> SectionList { get; set; }
        public List<DropdownViewModel> SubSectionList { get; set; }
    }
}
