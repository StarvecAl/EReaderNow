using EReaderNow.Data.AddDBMS;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;


namespace EReaderNow.Data.Domain
{
    public class BooksItem_ID
    {
        public int ID { get; set; }
        static public List<Genre> allGenre;
        [Required(ErrorMessage = "Заполните Название")]
        [Display(Name = "Название(Заголовок)")]
        public  string name { get; set; }
        [Required(ErrorMessage = "Заполните описание")]
        [MaxLength(1200, ErrorMessage = "Слишком длинно, не болеее 1200 символов")]
        [Display(Name = "Описание")]
        public  string desc { get; set; }
        [Required(ErrorMessage = "Заполните фото")]
        [Display(Name = "Ссылка на фото")]
        public  string img { get; set; }
        [Required(ErrorMessage = "Заполните Цена")]
        [Display(Name = "Цена")]
        public  ushort? price { get; set; }
        [Required(ErrorMessage = "Добавте хотя бы один жанр")]
        [Display(Name = "Жанр")]
        public   List<ListGenre_ID> Genres { get; set; }
        public AllDescriptionBooks textBooks { get; set; }
        public ICollection<Coments> Coments { get; set; }
    }
}
