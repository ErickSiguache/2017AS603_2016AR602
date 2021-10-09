using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using _2017AS603_2016AR602.Models;

namespace _2017AS603_2016AR602
{
    public class _2017AS603_2016AR602Context : DbContext
    {
        public _2017AS603_2016AR602Context(DbContextOptions<_2017AS603_2016AR602Context> options) : base(options)
        {
        }

        //Contexto de los metodos
        public DbSet<departamentos> departamentos { get; set; }
        public DbSet<facultad> facultad { get; set; }
        public DbSet<materias> materias { get; set; }
        public DbSet<alumnos> alumnos { get; set; }
        public DbSet<inscripciones> inscripciones { get; set; }
        public DbSet<notas> notas { get; set; }
    }
}
