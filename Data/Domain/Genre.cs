using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace EReaderNow.Data.Domain
{
    public class Genre
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Заполните поле жанр")]
        [Display(Name = "Жанр")]
        public string genreName { get; set; }

    }
}
