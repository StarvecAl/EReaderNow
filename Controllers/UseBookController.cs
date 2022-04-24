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

        public IActionResult Book(int id)
        {   
            var entity = id == default ? new BooksItem() : dataManager.BooksItems.GetBooksFieldById(id);
            return View(entity);
        }

        public IActionResult ReadBook(int id)
        {
            var entity = id == default ? new BooksItem() : dataManager.BooksItems.GetBooksFieldById(id);
            entity.textBooks.views++;
            var g = entity.textBooks;
            dataManager.BooksItems.SaveBookView(ref g);
            return View(entity);
        }
    }
}
