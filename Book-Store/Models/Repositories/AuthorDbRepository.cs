namespace Book_Store.Models.Repositories
{
    public class AuthorDbRepository : IBookstoreRepository<Author>
    {
        BookstoreDbContext db;

        public AuthorDbRepository(BookstoreDbContext _db)
        {
            db= _db;
        }


        public void Add(Author entity)
        {
            //entity.Id = db.Authors.Max(b => b.Id) + 1;

            db.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            var author = Find(id);

            db.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = db.Authors.SingleOrDefault(b => b.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public void Update(int id, Author newAuthor)
        {
            db.Update(newAuthor);
            db.SaveChanges();
        }
    }
}
