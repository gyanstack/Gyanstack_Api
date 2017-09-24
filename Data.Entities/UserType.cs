namespace Data.Entities
{
    public class UserType : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
