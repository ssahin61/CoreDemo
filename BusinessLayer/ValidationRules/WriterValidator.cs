using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
	public class WriterValidator : AbstractValidator<Writer>
	{
		public WriterValidator()
		{
			RuleFor(x => x.WriterName)
				.NotEmpty().WithMessage("Ad soyad kısmı boş geçilemez")
				.MinimumLength(5).WithMessage("İsim ve soy isim en az 5 karakterden oluşmalıdır")
				.MaximumLength(30).WithMessage("İsim ve soy isim en az 5 karakterden oluşmalıdır");

			RuleFor(x => x.WriterMail)
				.NotEmpty().WithMessage("E-posta alanı boş geçilemez.")
				.Must(WriterMail => WriterMail != null && WriterMail.Contains("@")).WithMessage("E-posta adresinde '@' sembolü bulunmalıdır.");

			RuleFor(x => x.WriterPassword)
				.NotEmpty().WithMessage("Şifre Boş Geçilemez")
				.MinimumLength(5).WithMessage("Şifre 5 karakterden küçük olamaz.")
				.MaximumLength(16).WithMessage("Şifre 16 karakterden büyük olamaz.")
				.Matches(@"[A-Z]+").WithMessage("Şifrede en az bir büyük harf olmalıdır.")
				.Matches(@"[a-z]+").WithMessage("Şifrede en az bir küçük harf olmalıdır.")
				.Matches(@"[0-9]+").WithMessage("Şifrede en az bir rakam olmalıdır");
		}
	}
}
