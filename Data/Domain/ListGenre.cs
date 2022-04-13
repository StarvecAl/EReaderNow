using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EReaderNow.Data.Domain
{
    public class ListGenre
    {
        public int ID { get; set; }
        public int? BooksItemID { get; set; }
        public int? GenreID { get; set; }
       
    }
}
