using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication6.Models
{
    public class Slider:BaseEntity
    {
       
        public string Title { get; set; }
        public string Descirption { get; set; }
        public string? ImageUrl { get; set; }
        public string RedirectorUrl { get; set; }
        public string RedirectorUrlText { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }




    }
}
