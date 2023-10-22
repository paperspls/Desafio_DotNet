using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Geo_WebApi_ASP.NET.Model
{
    public class Localidade
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(15)]
        public string CityCode { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(15)]
        public string State { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar")]
        [StringLength(80)]
        public string City { get; set; } = string.Empty;

        public virtual User? Usuario { get; set; }
    }
}
