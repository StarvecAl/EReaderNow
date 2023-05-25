using EReaderNow.Data.AddDBMS;
using Microsoft.AspNetCore.Mvc;


namespace EReaderNow.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;

        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return View(dataManager.BooksItems.GetBooksFields());
        }
        public IActionResult Index1()
        {
            return View();
        }


    }
}