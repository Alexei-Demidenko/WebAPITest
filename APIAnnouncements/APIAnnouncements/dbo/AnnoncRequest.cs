using System;
using System.ComponentModel.DataAnnotations;

namespace APIAnnouncements.dbo
{
    public class AnnoncRequest
    {
        //public int Number { get; set; }
        [Required(ErrorMessage = "Поля UserId является обязательным.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Поля Text является обязательным.")]
        [StringLength(150, MinimumLength = 15, ErrorMessage = "Длина имени  должна быть  >15 и <150 символов")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Поля Picture является обязательным.")]
        [RegularExpression(@"^\S.*\.(jpg|jpeg|webp)$", ErrorMessage = "Неверный формат файла.")]
        public string Picture { get; set; }

        [Required(ErrorMessage = "Поля Rating является обязательным.")]
        [Range(1, 5, ErrorMessage = "Значение поля Rating должна быть в диапозоне 1-5")]
        public int Rating { get; set; }
        //public DateTime CreationDate { get; set; }
        //public DateTime ExpirationDate { get; set; }
    }
}
