using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using WebApplication1.Extensions;

namespace WebApplication1.Controllers;

public class BookController : Controller
{
    private readonly BookDbContext _bookDbContext;

    public BookController(BookDbContext bookDbContext)
    {
        _bookDbContext = bookDbContext;
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = _bookDbContext.Books.Find(id);

        ViewBag.genres = new SelectList(_bookDbContext.Genres, "GenreId", "Name");

        var selectedAuthorsIds = _bookDbContext.BookAuthors.Where(x => x.BookId == id).Select(x => x.AuthorId);
        ViewBag.authors = new MultiSelectList(_bookDbContext.Authors, "AuthorId", "FullName", selectedAuthorsIds);

        return View(book);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var book = _bookDbContext.Books.Find(id);
        return View(book);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        var book = await _bookDbContext.Books.FindAsync(id);
        if (book != null) _bookDbContext.Books.Remove(book);
        await _bookDbContext.SaveChangesAsync();
        TempData["status"] = "Book DELETED!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Book book, IFormFile? image, int[] tags)
    {
        if (image != null)
        {
            var path = await FileUploadHelper.UploadAsync(image);
            book.ImageUrl = path;
        }

        book.Date = DateTime.Now;

        _bookDbContext.Books.Update(book);
        await _bookDbContext.SaveChangesAsync();


        var bookWithAuthors = _bookDbContext.Books.Include(x => x.BookAuthors)
            .FirstOrDefault(x => x.BookId == book.BookId);
        if (bookWithAuthors != null)
            _bookDbContext.UpdateManyToMany(
                bookWithAuthors.BookAuthors,
                tags.Select(x => new BookAuthor { BookId = book.BookId, AuthorId = x }),
                x => x.AuthorId
            );
        await _bookDbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.genres = new SelectList(_bookDbContext.Genres, "GenreId", "Name");
        ViewBag.authors = new MultiSelectList(_bookDbContext.Authors, "AuthorId", "FullName");
        return View();
    }

    [HttpGet]
    public IActionResult Index(int? genreId = null, int? authorId = null, int page = 1)
    {
        var books = _bookDbContext.Books.Include(x => x.BookAuthors).ThenInclude(x => x.Author).Include(x => x.Genre)
            .OrderByDescending(x => x.BookId);

        if (genreId != null) books = (IOrderedQueryable<Book>)books.Where(x => x.GenreId == genreId);

        if (authorId != null)
            books = (IOrderedQueryable<Book>)books.Where(x => x.BookAuthors.Any(bookAuthor => bookAuthor.AuthorId == authorId));


        var model = new IndexViewModel();

        var totalP = (int)Math.Ceiling(books.Count() / (double)model.LimitPage);
        books = (IOrderedQueryable<Book>)books.Skip((page - 1) * model.LimitPage).Take(model.LimitPage);


        model.Genres = _bookDbContext.Genres;
        model.Books = books;
        model.Authors = _bookDbContext.Authors;
        model.RecentBooks = _bookDbContext.Books.OrderByDescending(x => x.BookId).Take(model.LimitPage);
        model.CurrentPages = page;
        model.TotalPages = totalP;
        model.SelectedGenreId = genreId;
        model.SelectedAuthorId = authorId;


        return View(model);
    }


    [HttpGet]
    public IActionResult Details(int id)
    {
        var books = _bookDbContext.Books
            .Include(x => x.BookAuthors).ThenInclude(x => x.Author)
            .Include(x => x.Genre)
            .FirstOrDefault(books => books.BookId == id);
        return View(books);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Book book, IFormFile? image, int[] authors)
    {
        book.ImageUrl = await FileUploadHelper.UploadAsync(image);
        if (image != null)
        {
            var filename = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            await using var fs = new FileStream(@$"wwwroot/uploads/{filename}", FileMode.Create);
            await image.CopyToAsync(fs);
            book.ImageUrl = @$"/uploads/{filename}";
        }


        TempData["status"] = "New book added!";
        book.Date = DateTime.Now;
        await _bookDbContext.Books.AddAsync(book);
        await _bookDbContext.SaveChangesAsync();


        _bookDbContext.BookAuthors.AddRange(authors.Select(x => new BookAuthor { BookId = book.BookId, AuthorId = x }));

        await _bookDbContext.SaveChangesAsync();


        return RedirectToAction("Index");
    }
}