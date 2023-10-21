using Geo_WebApi_ASP.NET.Model;
using FluentValidation;

namespace Geo_WebApi_ASP.NET.Validator
{
    public class LocalidadeValidator : AbstractValidator<Localidade>
    {
        public LocalidadeValidator()
        {
            //RuleFor(l => l.Id);

            RuleFor(l => l.City)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(1000);

            RuleFor(l => l.State)
                    .NotEmpty()
                    .MinimumLength(5)
                    .MaximumLength(100);
        }
    }
}
