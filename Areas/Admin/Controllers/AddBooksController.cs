using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EReaderNow.Data.Domain;
using EReaderNow.Data.AddDBMS;
using EReaderNow.Data.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using EReaderNow.ViewModel;

namespace EReaderNow.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AddBooksController : Controller
    {   public ICollection<Genre> Genres { get; set; }
        private readonly DataManager dataManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        // GET: AddBook
        public AddBooksController(DataManager dataManager,IWebHostEnvironment hostingEnvironment)
        {
            this.dataManager = dataManager;
            this.hostingEnvironment= hostingEnvironment;
        }

        public IActionResult AddBook()
        {
            ViewBag.model = dataManager.BooksItems.GetGenre();
            return View();
        }
        public IActionResult TextBook()
        {
            ViewBag.Message = "Добавленно";
            return View();
           
        }
        public IActionResult AddGenre(Genre model)
        {
            Console.WriteLine(model.genreName);
            if (ModelState.IsValid)
            {
                    dataManager.BooksItems.SaveGenre(model);

                ViewBag.Message  = "Добавленно:"+model.genreName;
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(BooksItem model, IFormFile titleImageFile, int[] brands)
        {
           
           Console.WriteLine(model.ID);
            if (titleImageFile != null)
                {
                    model.img = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "img/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
            dataManager.BooksItems.SaveBooksField(model.textBooks);
            dataManager.BooksItems.SaveBooksField(model);
             Console.WriteLine(model.ID);
            if (brands != null)
            {
                
                foreach (var brand in brands)
                {
                    dataManager.BooksItems.SaveListGenre(model.ID, brand);
                }

            }
            else return View();
            
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());  
        }
    }
}
