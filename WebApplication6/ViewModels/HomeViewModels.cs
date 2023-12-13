using WebApplication6.Models;

namespace WebApplication6.ViewModels
{
    public class HomeViewModels
    {
        
            public List<Slider> Sliders { get; set; }
            public List<Service> Services { get; set; }
            public List<Genre> Genres { get; set; }
            public List<Author> Authors { get; set; }
            public List<Book> Books { get; set; }
            public List<Book> NewBooks { get; set; }
            public List<Book> FeaturedBooks { get; set; }
            public List<Book> BestsellerBooks { get; set; }
    }
}
