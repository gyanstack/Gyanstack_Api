namespace Api.Models
{
    public class SectionViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
    }
}
