using FluentValidation;
using Geo_WebApi_ASP.NET.Model;

namespace Geo_WebApi_ASP.NET.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
            public UserValidator()
            {
                RuleFor(u => u.Nome)
                    .NotEmpty()
                    .MaximumLength(255);

                RuleFor(u => u.Usuario)
                    .NotEmpty()
                    .EmailAddress();

                RuleFor(u => u.Senha)
                    .NotEmpty()
                    .MinimumLength(8);

                RuleFor(u => u.Foto)
                    .MaximumLength(5000);

            }
        }
}
