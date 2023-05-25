using EReaderNow.Data.AddDBMS;
using Microsoft.AspNetCore.Mvc;
using EReaderNow.Data.Domain;
namespace EReaderNow.Data.Test
{
    [Area("Admin")]
    public class AllTestController : Controller
    {
        private readonly DataManager dataManager;
        private short position = 0;

        public AllTestController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        
        public IActionResult IRepositoryCategory_Test()
        {   
            Console.WriteLine("Интеграционный тест");

            int[] a = { 1, 2, 3, 4, 5 };
            a[5] = 1;

            if (this.dataManager == null) return View("error/dataManager is null");

            Console.WriteLine("Посмотреть жанры 1. Посмотреть книги 2");
            int i = Convert.ToInt32(Console.ReadLine());
            if (i == 1)
            { foreach (var g in BooksItem.allGenre)
                { Console.WriteLine("ID жанра" + g.ID + "Навзание жанра" + g.genreName); }
            }
            if (i == 2)
            {
                Console.WriteLine("Введите id книги");
                int id = Convert.ToInt32(Console.ReadLine());
                BooksItem Book = dataManager.BooksItems.GetBooksFieldById(id);
                Console.WriteLine(Book.name + Book.Autor  + Book.desc);
            }
            return View();
            /*switch (position)
            {   
                case 1:
                    return ;
                    break;

                case 2:
                    return ;
                    break;

                case 3:
                    return View();
                    break;

                case 4:
                    return View();
                    break;

                case 5:
                    return View();
                    break;

                case 6:
                    return View();
                    break;

                case 7:
                    return View();
                    break;

                case 8:
                    return View();
                    break;

                case 9:
                    return View();
                    break;

                case 10:
                    return View();
                    break;

                case 11:
                    return View();
                    break;

                case 12:
                    return View();
                    break;

                case 13:
                    return View();
                    break;
                  

                default:
                    return View();
                    break;
            }*/
        }

      
    }
   
}
