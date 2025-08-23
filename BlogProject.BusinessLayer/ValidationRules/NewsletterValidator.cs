using BlogProject.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BusinessLayer.ValidationRules
{
    public class NewsletterValidator:AbstractValidator<Newsletter>
    {
        public NewsletterValidator()
        {
            RuleFor(x => x.Email)
          .NotEmpty().WithMessage("E-posta boş geçilemez")
          .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");
        }
    }
}
