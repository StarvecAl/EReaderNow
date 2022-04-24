
using EReaderNow.Interfaces;

using System.Text;
namespace EReaderNow.Models
{
  
    
    public class Books
    {
        int id { get; set; }
        int categoryID { get; set; }
       
        StringBuilder textBooks { get; set; }
        CategoryBooks categotyBooks { get; set; }
    }
    public class Piple
    {   int id { get; set; }
        decimal many { get; set; }
        string name { get; set; }
        string lastName { get; set; }
        string email { get; set; }
        string pasvord { get; set; }
    }
}
