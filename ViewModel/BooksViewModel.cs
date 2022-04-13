using EReaderNow.Models;

namespace EReaderNow.ViewModel
{
    public class BooksViewModel
    {
        public IEnumerable<CategoryBooks> allBooks { get; set; }
        public String Caregory { get; set; }

    }
}
