using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Geo_WebApi_ASP.NET.Model
{
    public class Localidade
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "char")]
        [StringLength(2)]
        public string State { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(80)]
        public string City { get; set; }
    }
}
