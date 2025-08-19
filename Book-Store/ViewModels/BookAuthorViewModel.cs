using Book_Store.Models;

namespace Book_Store.ViewModels
{
    public class BookAuthorViewModel
    {
        public int Bookid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
    }
}
