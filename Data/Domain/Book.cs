using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace EReaderNow.Data.Domain
{
    public abstract class Book
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Заполните название")]
        [Display(Name = "Название(Заголовок)")]
        public virtual string name { get; set; }
        [Required(ErrorMessage = "Заполните автора")]
        [Display(Name = "Автор")]
        public virtual string Autor { get; set; }
        [Required(ErrorMessage = "Заполните описание")]
        [Display(Name = "Описание")]
        public virtual string desc { get; set; }
        [Required(ErrorMessage = "Добавте ссылку на фото")]
        [Display(Name = "Ссылка на фото")]
        [BindNever]
        public virtual string img { get; set; }
        [Required(ErrorMessage = "Заполните поле Цена")]
        [Display(Name = "Цена")]
        public virtual ushort? price { get; set; }
        public virtual List<ListGenre> Genres { get; set; } 
    }
}
