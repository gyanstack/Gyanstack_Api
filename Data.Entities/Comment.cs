namespace Data.Entities
{
    public class Comment : EntityBase
    {
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public string UserComment { get; set; }

        public virtual Article Article { get; set; }
        public virtual User User { get; set; }
    }
}
