using System;
using System.ComponentModel.DataAnnotations;

namespace _2017AS603_2016AR602.Models
{
    public class inscripciones
    {
        [Key]
        public int id { get; set; }
        public int alumnoId { get; set; }
        public int materiaId { get; set; }
        public int inscripcion { get; set; }
        public DateTime fecha { get; set; }
        public char estado { get; set; }
    }
}
