using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Book
{
    public int BookId { get; set; }
    [Required] [MaxLength(50)] public string Title { get; set; }
    [MaxLength(1000)] public string Description { get; set; }
    public DateTime Date { get; set; }

    [Required]
    [Display(Name = "Photo url : ")]
    [DataType(DataType.Upload)]
    public string ImageUrl { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    public IEnumerable<BookAuthor> BookAuthors { get; set; }
}