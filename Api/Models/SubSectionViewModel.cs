using System.Collections.Generic;

namespace Api.Models
{
    public class SubSectionViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
        public string Section { get; set; }
        public int SectionId { get; set; }
        public List<DropdownViewModel> SectionList { get; set; }
    }
}
