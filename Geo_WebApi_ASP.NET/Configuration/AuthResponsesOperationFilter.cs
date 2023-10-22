using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Xml;

namespace Geo_WebApi_ASP.NET.Configuration
{
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!context.MethodInfo.GetCustomAttributes(true)
                .Any(options => options is AllowAnonymousAttribute))
            {
                operation.Security = new List<OpenApiSecurityRequirement> {
                    new OpenApiSecurityRequirement{
                        {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, Id = "JWT"
                            }
                        }, Array.Empty<string>()

                        }

                    }
                };
            }
        }
    }
}
