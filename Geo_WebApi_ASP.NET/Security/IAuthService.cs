using Geo_WebApi_ASP.NET.Model;

namespace Geo_WebApi_ASP.NET.Security
{
    public interface IAuthService
    {
        Task<UserLogin?> Autenticar(UserLogin userLogin);
    }
}
