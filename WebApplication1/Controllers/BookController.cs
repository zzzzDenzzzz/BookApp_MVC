using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers;

public class BookController(BookDbContext bookDbContext) : Controller
{
    [HttpGet]
    public IActionResult Index(int? genreId = null, int? authorId = null, int page = 1)
    {
        var books = bookDbContext.Books.Include(x => x.BookAuthors).ThenInclude(x => x.Author).Include(x => x.Genre)
            .OrderByDescending(x => x.BookId);

        if (genreId != null) books = (IOrderedQueryable<Book>)books.Where(x => x.GenreId == genreId);

        if (authorId != null)
            books = (IOrderedQueryable<Book>)books.Where(x =>
                x.BookAuthors.Any(bookAuthor => bookAuthor.AuthorId == authorId));


        var model = new IndexViewModel();

        var totalP = (int)Math.Ceiling(books.Count() / (double)model.LimitPage);
        books = (IOrderedQueryable<Book>)books.Skip((page - 1) * model.LimitPage).Take(model.LimitPage);


        model.Genres = bookDbContext.Genres;
        model.Books = books;
        model.Authors = bookDbContext.Authors;
        model.RecentBooks = bookDbContext.Books.OrderByDescending(x => x.BookId).Take(model.LimitPage);
        model.CurrentPages = page;
        model.TotalPages = totalP;
        model.SelectedGenreId = genreId;
        model.SelectedAuthorId = authorId;


        return View(model);
    }


    [HttpGet]
    public IActionResult Details(int id)
    {
        var books = bookDbContext.Books
            .Include(x => x.BookAuthors).ThenInclude(x => x.Author)
            .Include(x => x.Genre)
            .FirstOrDefault(books => books.BookId == id);
        return View(books);
    }
}