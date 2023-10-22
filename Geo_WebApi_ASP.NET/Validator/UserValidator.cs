using FluentValidation;
using Geo_WebApi_ASP.NET.Model;

namespace Geo_WebApi_ASP.NET.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Usuario)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Senha)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
