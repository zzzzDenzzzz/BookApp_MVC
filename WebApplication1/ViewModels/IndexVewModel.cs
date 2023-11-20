using WebApplication1.Models;

namespace WebApplication1.ViewModels;

public class IndexViewModel
{
    public IEnumerable<Book> Books { get; set; }
    public IEnumerable<Genre> Genres { get; set; }
    public IEnumerable<Author> Authors { get; set; }
    public IEnumerable<Book> RecentBooks { get; set; }
    public int CurrentPages { get; set; }
    public int? SelectedGenreId { get; set; }
    public int? SelectedAuthorId { get; set; }
    public int TotalPages { get; set; }
    public int LimitPage { get; set; } = 3;
}