using Book_Store.Models;
using Book_Store.Models.Repositories;
using Book_Store.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Book_Store.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookRepository;
        private readonly IBookstoreRepository<Author> authorRepository;
        private readonly IWebHostEnvironment hosting;



        public BookController(IBookstoreRepository<Book> bookRepository, IBookstoreRepository<Author> authorRepository,
            IWebHostEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;

        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();

            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);

            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                        fileName = model.File.FileName;
                        string fullPath = Path.Combine(uploads, fileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please select an author From The List !";

                        return View(GetAllAuthors());
                    }

                    var author = authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImageUrl = fileName
                    };
                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            ModelState.AddModelError("", "Please Fill All Required Fields");
            return View(GetAllAuthors());
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;

            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepository.List().ToList(),
                ImageUrl = book.ImageUrl

            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {
            try
            {
                string fileName = string.Empty;
                if (viewModel.File != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                    fileName = viewModel.File.FileName;
                    string fullPath = Path.Combine(uploads, fileName);
                    //Delete Old File
                    string oldFileName = bookRepository.Find(viewModel.BookId).ImageUrl;
                    string oldFullPath = Path.Combine(uploads, oldFileName);

                    if (fullPath != oldFullPath)
                    {
                        System.IO.File.Delete(oldFullPath);
                        //Save New File
                        viewModel.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                }



                var author = authorRepository.Find(viewModel.AuthorId);
                Book book = new Book
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = author,
                    ImageUrl = fileName

                };
                bookRepository.Update(viewModel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "-- Select Author --" });

            return authors;

        }

        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return vmodel;
        }
    }
}
