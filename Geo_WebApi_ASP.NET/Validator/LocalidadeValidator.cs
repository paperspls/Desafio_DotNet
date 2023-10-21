using Geo_WebApi_ASP.NET.Model;
using FluentValidation;

namespace Geo_WebApi_ASP.NET.Validator
{
    public class LocalidadeValidator : AbstractValidator<Localidade>
    {
        public LocalidadeValidator()
        {
            RuleFor(l => l.CityCode)
                    .NotEmpty()
                    .MinimumLength(1);

            RuleFor(l => l.City)
                    .NotEmpty()
                    .MinimumLength(1)
                    .MaximumLength(15);

            RuleFor(l => l.State)
                    .NotEmpty()
                    .MinimumLength(1)
                    .MaximumLength(80);
        }
    }
}
