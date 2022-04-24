using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EReaderNow.Data.Domain
{
    public class ListGenre
    {
        public int ID { get; set; }
        public Genre Genre { get; set; }
        public BooksItem BooksItem { get; set; }

    }
}
