namespace WebApplication6.Models
{
    public class BookImage:BaseEntity
    {
        
        public int BookId { get; set; }
        public bool? IsPoster { get; set; }
        public Book? book { get; set; }
        public string ImageUrl { get; set; }
   
    }
}
