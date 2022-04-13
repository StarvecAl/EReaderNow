using System.ComponentModel.DataAnnotations;

namespace EReaderNow.Data.Domain
{
    public class Category
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Заполните поле категория")]
        [Display(Name = "Категория")]
        public string categoryName { get; set; }
    }
}
