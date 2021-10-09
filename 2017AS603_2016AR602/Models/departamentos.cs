using System;
using System.ComponentModel.DataAnnotations;

namespace _2017AS603_2016AR602.Models
{
    public class departamentos
    {
        [Key]
        public int id { get; set; }
        public string departamento { get; set; }
    }
}
