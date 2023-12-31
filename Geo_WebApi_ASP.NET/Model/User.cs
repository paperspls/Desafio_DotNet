﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Geo_WebApi_ASP.NET.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Usuario { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Senha { get; set; } = string.Empty;
    }
}
