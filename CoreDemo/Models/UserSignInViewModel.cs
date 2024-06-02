using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
	public class UserSignInViewModel
	{
		[Required(ErrorMessage = "Lütfen kullanıcı adınızı girin!")]
		public string userName { get; set; }
		[Required(ErrorMessage = "Lütfen şifrenizi girin!")]
		public string password { get; set; }
	}
}
