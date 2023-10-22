using Geo_WebApi_ASP.NET.Data;
using Geo_WebApi_ASP.NET.Model;
using Microsoft.EntityFrameworkCore;

namespace Geo_WebApi_ASP.NET.Service.Implements
{
    public class LocalidadeService : ILocalidadeService
    {
        private readonly AppDbContext _context;

        public LocalidadeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Localidade?> Create(Localidade localidade)
        {
            await _context.Localidades.AddAsync(localidade);
            await _context.SaveChangesAsync();

            return localidade;
        }
        public async Task Delete(Localidade localidade)
        {
            _context.Remove(localidade);

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Localidade>> GetAll()
        {
            return await _context.Localidades
             
                .ToListAsync();
        }
        public async Task<Localidade?> GetById(long id)
        {
            try
            {
                var localidade = await _context.Localidades
                 
                    .FirstAsync(i => i.Id == id);

                return localidade;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Localidade?> Update(Localidade localidade)
        {
            var localidadeUpdate = await _context.Localidades.FindAsync(localidade.Id);

            if (localidadeUpdate == null)
                return null;

            if (localidade is not null)
            {
                var buscaCategoria = await _context.Localidades.FindAsync(localidade.Id);

                if (buscaCategoria == null)
                    return null;
            }

            await _context.SaveChangesAsync();

            return localidade;
        }
        public async Task<IEnumerable<Localidade>> GetByCity(string city)
        {
            var localidade = await _context.Localidades
                .Where(l => l.City.Contains(city))
         
                .ToListAsync();

            return localidade;
        }
        public async Task<IEnumerable<Localidade>> GetByState(string state)
        {
            var localidade = await _context.Localidades
                .Where(l => l.State.Contains(state))
            
                .ToListAsync();

            return localidade;
        }

        public async Task<IEnumerable<Localidade>> GetByCityCode(string citycode)
        {
            var localidade = await _context.Localidades
                .Where(l => l.CityCode.Contains(citycode))
           
                .ToListAsync();

            return localidade;
        }
    }

    }

