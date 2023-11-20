using Microsoft.AspNetCore.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewComponents;

public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(int currentPage, int totalPages, int limit, int? authorId, int? genreId)
    {
        var paginationViewModel = new PaginationViewModel()
        {
            TotalPages = totalPages,
            CurrentPage = currentPage,
            LimitItem = limit,
            AuthorId = authorId,
            GenreId = genreId
        };
        return View("Pagination", paginationViewModel);
    }
}