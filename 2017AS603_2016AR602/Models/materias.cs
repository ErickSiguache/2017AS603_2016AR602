using System;
using System.ComponentModel.DataAnnotations;

namespace _2017AS603_2016AR602.Models
{
    public class materias
    {
        [Key]
        public int id { get; set; }
        public int facultadId { get; set; }
        public string materia { get; set; }
        public int unidades_valorativas { get; set; }
        public char estado { get; set; }
    }
}
