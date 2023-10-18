using Microsoft.EntityFrameworkCore;

namespace Geo_WebApi_ASP.NET.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            { 

            }
    }
}
