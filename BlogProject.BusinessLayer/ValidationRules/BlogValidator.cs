using BlogProject.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BusinessLayer.ValidationRules
{
    public class BlogValidator: AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.BlogTitle)
                .NotEmpty().WithMessage("Blog başlığı boş geçilemez.")
                .MaximumLength(150).WithMessage("Blog başlığı en fazla 150 karakter olabilir.")
                .MinimumLength(5).WithMessage("Blog başlığı en az 5 karakter olmalıdır.");

            RuleFor(x => x.BlogContent)
                .NotEmpty().WithMessage("Blog içeriği boş geçilemez.")
                .MinimumLength(20).WithMessage("Blog içeriği en az 20 karakter olmalıdır.");

            RuleFor(x => x.BlogImage)
                .NotEmpty().WithMessage("Kapak görseli boş geçilemez.")
                .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("Geçerli bir kapak görseli linki giriniz.");

            RuleFor(x => x.BlogThumnailImage)
                .NotEmpty().WithMessage("Thumbnail görseli boş geçilemez.")
                .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("Geçerli bir thumbnail linki giriniz.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Kategori seçilmelidir.")
                .GreaterThan(0).WithMessage("Kategori seçilmelidir.");
        }
    }
}
