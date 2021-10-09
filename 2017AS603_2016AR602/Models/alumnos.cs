using System;
using System.ComponentModel.DataAnnotations;

namespace _2017AS603_2016AR602.Models
{
    public class alumnos
    {
        [Key]
        public int id { get; set; }
        public string carnet { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public int dui { get; set; }
        public int departamentoId { get; set; }
        public int estado { get; set; }
    }
}
