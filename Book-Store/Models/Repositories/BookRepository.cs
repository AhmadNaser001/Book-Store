
namespace Book_Store.Models.Repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {
        IList<Book> books;
        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id = 1,Title = "C# Programming",Description = "A comprehensive guide to C# programming.", Author = new Author()
                },
                new Book
                {
                    Id = 2,Title = "PHP Programming",Description = "A comprehensive guide to PHP programming.", Author = new Author()
                },
                new Book
                {
                    Id = 3,Title = "JAVA Programming",Description = "A comprehensive guide to JAVA programming.", Author = new Author()
                },
            };
            
        }



        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int id,Book newBook)
        {
            var book = Find(id);

            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author; 
        }
    }
}
