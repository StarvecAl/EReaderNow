using Microsoft.AspNetCore.Mvc;
using EReaderNow.Data.AddDBMS;
using EReaderNow.Data.Domain;

namespace EReaderNow.Controllers
{
    public class BooksController : Controller
    {
        
        private readonly DataManager dataManager;

        public BooksController(DataManager dataManager)
        {
           /* BooksItem.allGenre = dataManager.BooksItems.GetGenre().ToList();*/
            this.dataManager = dataManager;
        }
        public IActionResult Home()
        {
            @ViewBag.genreTag = "Все книги";

            return View(dataManager.BooksItems.GetBooksInclude().ToList());
        }
        public IActionResult genre()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Home(string genre)
        {
            var genre1 = new Genre();
            @ViewBag.genreTag = genre;
            foreach (var g in BooksItem.allGenre)
            {
                if (g.genreName == genre)
                    genre1 = g;
            }
            return View(dataManager.BooksItems.GetBooksGnenre(genre1.ID));
        }
    
        public IActionResult Index()
        {
            return View(dataManager.BooksItems.GetBooksInclude());
        }
    }
}
