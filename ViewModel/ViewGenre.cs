using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EReaderNow.ViewModel
{
    public class ViewGenre
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Заполните поле жанр")]
        [Display(Name = "Жанр")]
        public string genreName { get; set; }
    }
}
