using System.ComponentModel.DataAnnotations;

namespace EReaderNow.Data.Domain
{
    public class Coments
    {

        public int Id { get; set; }
        public String Com { get; set; }
        public String User { get; set; }
        public BooksItem BooksItem { get; set; }
    }
}
