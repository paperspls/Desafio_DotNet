using Geo_WebApi_ASP.NET.Model;

namespace Geo_WebApi_ASP.NET.Service
{
    public interface ILocalidadeService
    {
    Task<IEnumerable<Localidade>> GetAll();
    Task<Localidade?> GetById(long id);
    Task<IEnumerable<Localidade>> GetByCityCode(string citycode);
    Task<IEnumerable<Localidade>> GetByCity(string city);
    Task<IEnumerable<Localidade>> GetByState(string state);
    Task<Localidade?> Create(Localidade localidade);
    Task Delete(Localidade localidade);
    Task<Localidade?> Update(Localidade localidade);
        

    }
}
