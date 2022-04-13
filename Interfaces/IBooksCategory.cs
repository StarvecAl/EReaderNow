using EReaderNow.Models;

namespace EReaderNow.Interfaces
{
    public interface IBooksCategory
    {
        IEnumerable<CategoryBooks> GetCategory { get; }
    }
}
