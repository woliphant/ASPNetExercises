using Microsoft.AspNet.Mvc;
using ASPNetExercises.Models;
using ASPNetExercises.ViewModels;

namespace ASPNetExercises.Controllers
{
    public class MenuItemController : Controller
    {
        AppDbContext _db;
        public MenuItemController(AppDbContext context)
        {
            _db = context;
        }
        // GET: /<controller>/
        public IActionResult Index(CategoryViewModel category)
        {
            MenuItemModel model = new MenuItemModel(_db);
            MenuItemViewModel viewModel = new MenuItemViewModel();
            viewModel.CategoryName = category.CategoryName;
            viewModel.MenuItems = model.GetAllByCategoryName(category.CategoryName);
            return View(viewModel);
        }
    }
}