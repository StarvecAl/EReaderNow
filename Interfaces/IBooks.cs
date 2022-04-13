using EReaderNow.Models;

namespace EReaderNow.Interfaces
{
    public interface IBooks
    {
        IEnumerable<Books> GetBooks { get; set; }
    }
}
