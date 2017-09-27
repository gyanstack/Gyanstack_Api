namespace Api.Dto
{
    public class UserComment
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string Comment { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
    }
}
