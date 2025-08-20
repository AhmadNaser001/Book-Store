
namespace Book_Store.Models.Repositories
{
    public class AuthorRepository : IBookstoreRepository<Author>
    {
        IList<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author
                {
                    Id = 1,FullName = "Ahmad Naser",
                },
                new Author
                {
                    Id = 2,FullName = "Mahmoud Naser",
                },
                new Author
                {
                    Id = 3,FullName = "Khaled Naser",
                },
            };
            
        }



        public void Add(Author entity)
        {
            entity.Id = authors.Max(b => b.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(b => b.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(int id,Author newAuthor)
        {
            var author = Find(id);

            author.FullName = newAuthor.FullName;
        }
    }
}
