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

            this.dataManager = dataManager;
        }
       
        public IActionResult Home()
        {
            
            if (RouteData.Values["filtr"] != null)
            {
                Console.WriteLine(RouteData.Values["filtr"]);
                
            }
            ViewBag.genreTag = new Genre() { genreName = "Все книги", ID = 0 };
            int genre = 0;
            int skip = 1;
            if (RouteData.Values["strange"] != null)
            {
                skip = Convert.ToInt32(RouteData.Values["strange"]);
            }
            if (RouteData.Values["id"] != null)
            {
                genre = Convert.ToInt32(RouteData.Values["id"]);
            }
            if (genre != 0)
            {
                var genre1 = new Genre();
                foreach (var g in BooksItem.allGenre)
                {
                    if (g.ID == genre)
                        genre1 = g;
                }
               ViewBag.genreTag = genre1;

                return View(dataManager.BooksItems.GetBooksGnenre(genre, skip));
            }
       

            return View(dataManager.BooksItems.GetBooksInclude(skip).ToList());
        }

        public IActionResult genre()
        {
            return View();
        }
        /* [Route("Books/Home/{id }/{strange}")]*/
        [HttpPost]
        public IActionResult Home(int? id, string? filtr)
        {   

            ViewBag.genreTag = new Genre() { genreName = "Все книги", ID = 0 };
            int skip = 1;
            if (id != null)
            {
                var genre1 = new Genre();
                foreach (var g in BooksItem.allGenre)
                {
                    if (g.ID == id)
                        genre1 = g;
                }
                @ViewBag.genreTag = genre1;
            }
          
            return View(dataManager.BooksItems.GetBooksGnenre(id, skip, filtr));
                    
        }
        public IActionResult Index(int i = 1)
        {
            return View(dataManager.BooksItems.GetBooksInclude(i));
        }
    }
}
