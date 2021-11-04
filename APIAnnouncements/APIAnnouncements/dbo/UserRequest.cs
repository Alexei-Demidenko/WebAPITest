using System.ComponentModel.DataAnnotations;

namespace APIAnnouncements.dbo
{
	public class UserRequest
	{
        [Required(ErrorMessage = "Поля Name является обязательным.")]
		[StringLength(25, MinimumLength = 3, ErrorMessage = "Длина имени  должна быть  >3 и <25 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поля Admin является обязательным.")]
        [RegularExpression(@"^(?i:true|false)$", ErrorMessage = "Допустимы только значения true или false.")]
		public bool Admin { get; set; }
	}
}
