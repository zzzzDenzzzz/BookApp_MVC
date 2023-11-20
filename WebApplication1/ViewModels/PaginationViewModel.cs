namespace WebApplication1.ViewModels;

public class PaginationViewModel
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int? AuthorId { get; set; }
    public int? GenreId { get; set; }
    public int LimitItem { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public Dictionary<string, string> RouteParams { get; set; }
}