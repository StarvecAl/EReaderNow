using EReaderNow.Data.AddDBMS;
using EReaderNow.Data.Domain;
using Microsoft.AspNetCore.Mvc;


namespace EReaderNow.Controllers
{
    public class UseBookController : Controller 
    {
        private readonly DataManager dataManager;

        public UseBookController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Books()
        {
            var g = dataManager.AllDescriptionBooks.GetBooksFieldById(1);
            g.views++;
            dataManager.BooksItems.SaveBookView(ref g);
            return View(dataManager.BooksItems.GetBooksInclude());
            
        }

        public IActionResult Book(int id)
        {
            var entity = id == default ? new BooksItem() : dataManager.BooksItems.GetBooksFieldById(id);
            return View(entity);
        }
    }
}
