using System;
using System.ComponentModel.DataAnnotations;

namespace _2017AS603_2016AR602.Models
{
    public class notas
    {
        [Key]
        public int id { get; set; }
        public int inscripcionId { get; set; }
        public string evaluacion { get; set; }
        public Decimal nota { get; set; }
        public Decimal porcentaje { get; set; }
        public DateTime fecha { get; set; }
    }
}
