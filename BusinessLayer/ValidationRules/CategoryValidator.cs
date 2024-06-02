using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
	public class CategoryValidator : AbstractValidator<Category>
	{
		public CategoryValidator()
		{
			RuleFor(x => x.CategoryName)
				.NotEmpty().WithMessage("Kategori adını boş geçemezsiniz")
				.MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır")
				.MaximumLength(30).WithMessage("Kategori adı en fazla 30 karakter olmalıdır");

			RuleFor(x => x.CategoryDescription)
				.NotEmpty().WithMessage("Kategori açıklamasını boş geçemezsiniz");
		}
	}
}
