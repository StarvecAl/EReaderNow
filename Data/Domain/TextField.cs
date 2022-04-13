using System.ComponentModel.DataAnnotations;

namespace EReaderNow.Data.Domain
{
    public class TextField : EntityBase
    {
        [Required]
        public string codeWord { get; set; }

        [Display(Name = "Название страницы (заголовок)")]
        public override string title { get; set; } = "Информационная страница";

        [Display(Name = "Cодержание страницы")]
        public override string text { get; set; } = "Содержание заполняется администратором";
    }
}
