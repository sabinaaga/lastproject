namespace Fiorello.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;
        public string ImageUrl { get; set; }
    }
}
