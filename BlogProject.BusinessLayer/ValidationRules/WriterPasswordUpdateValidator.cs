using BlogProject.DTOLayer.WriterDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BusinessLayer.ValidationRules
{
    public class WriterPasswordUpdateValidator : AbstractValidator<WriterPasswordUpdateDto>
    {
        public WriterPasswordUpdateValidator()
        {
            // Profil bilgileri
            RuleFor(x => x.WriterName)
                .NotEmpty().WithMessage("İsim Soyisim boş geçilemez")
                .MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır")
                .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olmalıdır");

            RuleFor(x => x.WriterEmail)
                .NotEmpty().WithMessage("E-posta boş geçilemez")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");

            RuleFor(x => x.WriterAbout)
                .MaximumLength(250).WithMessage("Hakkında alanı en fazla 250 karakter olabilir");

            RuleFor(x => x.WriterImage)
                .MaximumLength(100).WithMessage("Görsel URL alanı en fazla 100 karakter olabilir");
            RuleFor(x => x.NewPassword)
                .MinimumLength(6).WithMessage("Yeni şifre en az 6 karakter olmalıdır")
                .Matches("[A-Z]").WithMessage("Yeni şifre en az bir büyük harf içermelidir")
                .Matches("[a-z]").WithMessage("Yeni şifre en az bir küçük harf içermelidir")
                .Matches("[0-9]").WithMessage("Yeni şifre en az bir rakam içermelidir")
                .Matches("[^a-zA-Z0-9]").WithMessage("Yeni şifre en az bir özel karakter içermelidir");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Şifreler uyuşmuyor");
        }
    }
}
