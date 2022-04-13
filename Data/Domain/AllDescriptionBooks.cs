using System.ComponentModel.DataAnnotations;
using System.Text;
namespace EReaderNow.Data.Domain
{
    public class AllDescriptionBooks
    {

        public int ID { get; set; }
        [Required(ErrorMessage = "Заполните поле Текст")]
        [Display(Name = "Текст")]
        public string textBook { get; set; }
        public int? views  { get; set; }
        public AllDescriptionBooks() { views = 0; }
    }
}
