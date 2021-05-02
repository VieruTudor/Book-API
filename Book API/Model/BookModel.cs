using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book_API.Model
{
    public class BookModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Year must be greater than 0")]
        public int Year { get; set; }
    }
}
