namespace WebApplication1.Models;

public class BookAuthor
{
    public int BookAuthorId { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}