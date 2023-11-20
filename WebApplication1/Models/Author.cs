namespace WebApplication1.Models;

public class Author
{
    public int AuthorId { get; set; }
    public string FullName { get; set; }
    public IEnumerable<BookAuthor> BookAuthors { get; set; }
}