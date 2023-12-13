using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication6.Models
{
    public class Book:BaseEntity
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsAvailable { get; set; }
        public double Tax { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DisPrice { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public int GenreId { get; set; }
        public bool isNew { get; set; }
        public bool isFeatured { get; set; }
        public bool isBestseller { get; set; }
        public Genre? Genre { get; set; }
        public List<BookTag>? BookTags { get; set; }
        [NotMapped]
        public List<int>? TagIds { get; set; }
        public List<BookImage>? BookImages { get; set; }
        [NotMapped]
        public IFormFile? FaceImage { get; set; }
        [NotMapped]
        public IFormFile? BackViewImg { get; set; }
        [NotMapped]
        public List<IFormFile>? BookDetailImgs { get; set; }
        [NotMapped]
        public List<int>? BookImagesIds { get; set; }



    }
}