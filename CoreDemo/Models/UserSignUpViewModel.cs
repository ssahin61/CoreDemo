using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
	public class UserSignUpViewModel
	{
		[Display(Name = "Ad Soyad")]
		[Required(ErrorMessage = "Lütfen Ad Soyad giriniz !")]
		public string NameSurname { get; set; }

		[Display(Name = "Parola")]
		[Required(ErrorMessage = "Lütfen Parola giriniz !")]
		public string Password { get; set; }

		[Display(Name = "Parolayı doğrula")]
		[Required(ErrorMessage = "Parola uyuşmuyor, Tekrar deneyiniz.")]
		public string ConfirmPassword { get; set; }

		[Display(Name = "Mail")]
		[Required(ErrorMessage = "Lütfen Mail giriniz !")]
		public string Mail { get; set; }

		[Display(Name = "Kullanıcı Adı")]
		[Required(ErrorMessage = "Lütfen Kullanıcı Adı giriniz !")]
		public string UserName { get; set; }
	}
}
