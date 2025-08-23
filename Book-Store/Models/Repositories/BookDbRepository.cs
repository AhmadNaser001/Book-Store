using Microsoft.EntityFrameworkCore;

namespace Book_Store.Models.Repositories
{
    public class BookDbRepository : IBookstoreRepository<Book>
    {
        BookstoreDbContext db;

        public BookDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Book entity)
        {
            //entity.Id = db.Books.Max(b => b.Id) + 1;

            db.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            var book = Find(id);

            db.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public void Update(int id, Book newBook)
        {
            db.Update(newBook);
            db.SaveChanges();
        }
    }
}

