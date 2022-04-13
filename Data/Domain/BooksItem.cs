using EReaderNow.Data.AddDBMS;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace EReaderNow.Data.Domain
{
    public class BooksItem : Book
    {
        static public List<Genre> allGenre;
        [Required(ErrorMessage = "Заполните Название")]
        [Display(Name = "Название(Заголовок)")]
        public override string name { get; set; }
        [Required(ErrorMessage = "Заполните описание")]
        [MaxLength(1200, ErrorMessage = "Слишком длинно, не болеее 1200 символов")]
        [Display(Name = "Описание")]
        public override string desc { get; set; }
        [Required(ErrorMessage = "Заполните фото")]
        [Display(Name = "Ссылка на фото")]
        public override string img { get; set; }
        [Required(ErrorMessage = "Заполните Цена")]
        [Display(Name = "Цена")]
        public override ushort? price { get; set; }
        [Required(ErrorMessage = "Добавте хотя бы один жанр")]
        [Display(Name = "Жанр")]
        public override List<ListGenre> Genres { get; set; }
        public AllDescriptionBooks textBooks { get; set; }
        public ICollection<Coments> Coments { get; set; }
    }
}
