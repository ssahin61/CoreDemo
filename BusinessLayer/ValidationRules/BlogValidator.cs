using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
	public class BlogValidator : AbstractValidator<Blog>
	{
		public BlogValidator()
		{
			RuleFor(x => x.BlogTitle)
				.NotEmpty().WithMessage("Blog başlığını boş geçemezsiniz")
				.MaximumLength(50).WithMessage("150 karakterden daha az veri girişi yapın")
				.MinimumLength(5).WithMessage("5 karakterden daha fazla veri girişi yapın");

			RuleFor(x => x.BlogContent)
				.NotEmpty().WithMessage("Blog içeriğini boş geçemezsiniz")
				.MaximumLength(1000).WithMessage("1000 karakterden daha az veri girişi yapın")
				.MinimumLength(10).WithMessage("10 karakterden daha fazla veri girişi yapın");

			RuleFor(x => x.BlogImage)
				.NotEmpty().WithMessage("Blog görselini boş geçemezsiniz");

			RuleFor(x => x.BlogThumbnailImage)
				.NotEmpty().WithMessage("Küçük blog görselini boş geçemezsiniz");
		}
	}
}
  