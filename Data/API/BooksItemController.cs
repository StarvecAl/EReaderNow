using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EReaderNow.Data.AddDBMS;
using EReaderNow.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using EReaderNow.Data.Repository;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Internal;
using EReaderNow.Data.Service;
using ERederNow_android.Models;
using Microsoft.AspNetCore.Identity;
using EReaderNow.ViewModel;
using EReaderNow.Models;
using System.Net;
using System.Net.Http;

namespace EReaderNow.Data.API
{
    public interface IDbContextFactory
    {
        AddDB Create();
    }


    public class DbContextFactory : IDbContextFactory
    {
     
        private readonly IConfiguration _config;
        
        public DbContextFactory(IConfiguration config)
        {
            _config = config;
        }

        public AddDB Create()
        {
            // Подключаем json конфиг
            string connectionString = Config.ConnectionBDStrings;
            System.Console.WriteLine(connectionString);
            DbContextOptionsBuilder<AddDB> builder = new DbContextOptionsBuilder<AddDB>();
            builder.UseSqlServer(connectionString);
            System.Console.WriteLine("DbContextFactory");
            return new AddDB(builder.Options);
        }
    }
   
    [Route("api/[controller]")]
    [ApiController]
    public class BooksItemController : ControllerBase
    {
       
        private readonly IDbContextFactory _dbContextFactory;
        private readonly AddDB db;
      public BooksItemController(IDbContextFactory dbContextFactor)
        {
            System.Console.WriteLine("DB API Connect");
            _dbContextFactory = dbContextFactor;
         
       
        }
      
        // Create an instance of JsonSerializerOptions
        static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false,
            ReferenceHandler = ReferenceHandler.Preserve,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,

            // Other options as needed
        };
        [HttpGet("APIBooks/{page:int}/{genre:int?}/{filtr?}")]
        public async Task<object> GetByAll(int page, int? genre, string? filtr)
        {
            /*using (var scope = ServiceScopeFactory.CreateScope())
            {
                var _logService = scope.ServiceProvider.GetService<AddDB>();
                await _logService.AddLog(context);
            }*/

            short take = 2;
            System.Console.WriteLine("GetByAll");
            using (AddDB db = _dbContextFactory.Create())
            {

                var query = db.BooksItem.AsQueryable();

                if (genre != null && genre != 0)
                {
                    Console.WriteLine(genre);
                    query = query.Where(g => g.ID == genre.Value);
                }

                if (!string.IsNullOrEmpty(filtr))
                {
                    Console.WriteLine(filtr);
                    query = query.Where(x => x.name.Contains(filtr));
                }
                if (page >= 2)
                {
                    query = query.Skip(page * take - take);
                }
                Console.WriteLine(query);
                var books = await query.Include(x => x.Genres).ThenInclude(genre => genre.Genre).Take(take).ToListAsync();
               /* var booksWithGenres = query.Select(book => new BooksItem
                {
                    Genres = book.Genres.Select(listGenre => new ListGenre
                    {
                        ID = listGenre.ID,
                        Genre = new Genre
                        {
                            ID = listGenre.Genre.ID,
                            genreName = listGenre.Genre.genreName
                        }
                    }).ToList()
                }).ToList();*/
                foreach (BooksItem a in books) { Console.WriteLine(a.Genres); foreach (ListGenre b in a.Genres) Console.WriteLine(b.Genre.ID); }
           
                /*   string result = JsonSerializer.Serialize<IEnumerable<BooksItem>>(books, options);*/
                var jsonBytes = JsonSerializer.SerializeToUtf8Bytes<IEnumerable<BooksItem>>(books, options);
           
                return new FileContentResult(jsonBytes, "application/json");
            }


        }

        // GET api/BooksItems/5
        [HttpGet("APIBook/{ID:int}")]
        public async Task<ActionResult<BooksItem>> GetByID(int ID)
        {
            using (AddDB db = _dbContextFactory.Create())
           
            {
                if (ID == null)
                {
                    return NotFound();
                }
                System.Console.WriteLine("Book");
                BooksItem Books = await db.BooksItem.Include(x => x.textBooks).Include(x=> x.Genres).FirstOrDefaultAsync(x => x.ID == ID);

                System.Console.WriteLine("Book1");
                if (Books == null)
                    return NotFound();

                return new ObjectResult(Books);
            }
        }
        // GET api/AllGenres
        [HttpGet("GetAllGenres/")]
        public async Task<List<Genre>> GetAllGenres()
        {
            using (AddDB db = _dbContextFactory.Create())
            {
                System.Console.WriteLine("GetAllGenres");
                var AllGenres = await db.genres.ToListAsync();
                System.Console.WriteLine(AllGenres);
                return AllGenres;
            }
        }

       
        public async Task<string> DownloadAndSaveImage(string imageUrl)
        {
            try
            {
                using (HttpClient _httpClient = new HttpClient())
                using (var response = await _httpClient.GetAsync(imageUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Download the image from the URL
                        var imageBytes = await response.Content.ReadAsByteArrayAsync();

                        // Save the image to a file
                        var fileName = $"{Guid.NewGuid()}.jpg"; // Generate a unique file name
                        var filePath = Path.Combine("C:\\Users\\Starvec\\source\\repos\\Text2\\Text2\\wwwroot\\img\\", fileName); // Replace "path" with the path where you want to save the file
                        await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
               
                        return fileName;
                    }
                    else return "False response.IsSuccessStatusCode";
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
                return "False catch";
            }
        }
        [HttpPost("Add/")]
        [Obsolete]
        public async Task<ActionResult> Post(object BooksItem)
        {
            using (AddDB db = _dbContextFactory.Create())
            {
                Console.WriteLine("Add");
                Console.WriteLine(BooksItem);
                BooksItem book = JsonSerializer.Deserialize<BooksItem>(BooksItem.ToString()); 
                Console.WriteLine(book);
                if (book == null)
                {
                    return BadRequest();
                }
                var r = await DownloadAndSaveImage(book.img);
                book.img = r;
                Console.WriteLine(r);
                db.BooksItem.Add(book);
                await db.SaveChangesAsync();
                return Ok(book);
            }
        }
     /*   [HttpPost("OnLogin")]
        public async Task<IActionResult> OnLogin(object log)
        {
            Console.WriteLine("OnLogin");
            Console.WriteLine(log.ToString());

            Account account = JsonSerializer.Deserialize<Account>(log.ToString());
            Console.WriteLine(account.UserName);
            using (AddDB db = _dbContextFactory.Create())
            {
                if (log == null)
                {
                    return BadRequest();
                }

            }
            return Ok(log);

        }*/



        // PUT api/BooksItems/
        [HttpPut]
        public async Task<ActionResult<BooksItem>> Put(object book)
        {
            
            BooksItem BooksItem = book as BooksItem;
            Console.WriteLine("Update");
            if (BooksItem == null)
            {
                return BadRequest();
            }
            if (!db.BooksItem.Any(x => x.ID == BooksItem.ID))
            {
                return NotFound();
            }

            db.Update(BooksItem);
            await db.SaveChangesAsync();
            return Ok(BooksItem);
        }
       /* [HttpPut("PutLogin/")]
        public async Task<ActionResult<BooksItem>> PutLogin(BooksItem BooksItem)
        {
            Console.WriteLine("PutLogin");
            using (AddDB db = _dbContextFactory.Create())
            {
                if (BooksItem == null)
                {
                    return BadRequest();
                }
                if (!db.BooksItem.Any(x => x.ID == BooksItem.ID))
                {
                    return NotFound();
                }

                db.Update(BooksItem);
                await db.SaveChangesAsync();
            }
            return Ok(BooksItem);

        }*/
        // DELETE api/BooksItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BooksItem>> DeleteBooksByID(int ID)
        {
            using (db)
            {
                BooksItem BooksItem = db.BooksItem.FirstOrDefault(x => x.ID == ID);
                if (BooksItem == null)
                {
                    return NotFound();
                }
                db.BooksItem.Remove(BooksItem);
                await db.SaveChangesAsync();
                return Ok(BooksItem);
            }

        }
        

    }
}