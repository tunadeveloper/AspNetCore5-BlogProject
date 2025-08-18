using BlogProject.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName)
            .NotEmpty().WithMessage("İsim Soyisim boş geçilemez")
            .MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır")
            .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olmalıdır");

            RuleFor(x => x.WriterEmail)
           .NotEmpty().WithMessage("E-posta boş geçilemez")
           .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");

            RuleFor(x => x.WriterPassword)
           .NotEmpty().WithMessage("Şifre boş geçilemez")
           .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
           .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir")
           .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir")
           .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir")
           .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir (!, ?, *, vs.)");

            RuleFor(x => x.WriterAbout)
           .MaximumLength(250).WithMessage("Hakkında alanı en fazla 250 karakter olabilir");

            RuleFor(x => x.WriterImage)
           .MaximumLength(100).WithMessage("Hakkında alanı en fazla 250 karakter olabilir");
        }
    }
}
