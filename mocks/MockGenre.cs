using EReaderNow.Interfaces;
using EReaderNow.Models;

namespace EReaderNow.mocks
{
    public class MockGenre : IGenre
    {
        static public IEnumerable<String> GetGenry
        {
            get {
               
                return new List<String> {
                     new String("Фентези"),
                     new String("Фантастика")
                                                  };
            }
            set
            {

            }
    }
    }
}
