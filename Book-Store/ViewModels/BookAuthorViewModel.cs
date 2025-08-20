using Book_Store.Models;
using System.ComponentModel.DataAnnotations;

namespace Book_Store.ViewModels
{
    public class BookAuthorViewModel
    {
        public int Bookid { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }


        [Required]
        [StringLength(120,MinimumLength =5)]

        public string Description { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
    }
}
