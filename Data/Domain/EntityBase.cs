using System;
using System.ComponentModel.DataAnnotations;

namespace EReaderNow.Data.Domain
{
    public abstract class EntityBase
    {
        protected EntityBase() => DateAdded = DateTime.UtcNow;

        [Required]
        public Guid ID { get;set; }

        [Display(Name = "Название (заголовок)")]
        public virtual string title { get; set; }


        [Display(Name = "Полное описание")]
        public virtual string text { get; set; }

        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
