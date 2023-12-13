using WebApplication6.Models;

namespace WebApplication6.ViewModels
{
    public class ProductDetailViewModel
    {
        public Book Book { get; set; }
        public List<Book> RelatedBooks { get; set; }
    }
}
