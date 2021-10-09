using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2017AS603_2016AR602.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2017AS603_2016AR602.Controllers
{
    public class inscripcionesController : Controller
    {
        private readonly _2017AS603_2016AR602Context _contexto;
        public inscripcionesController(_2017AS603_2016AR602Context miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Busqueda en general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/inscripciones")]
        public IActionResult Get()
        {
            var inscripciones = from e in _contexto.inscripciones
                          join alum in _contexto.alumnos on e.alumnoId equals alum.id
                          join mat in _contexto.materias on e.materiaId equals mat.id
                          select new
                          {
                              e.id,
                              alum.nombre,
                              mat.materia,
                              e.inscripcion,
                              e.fecha,
                              e.estado
                          };
            if (inscripciones.Count() > 0)
            {
                return Ok(inscripciones);
            }
            return NotFound();
        }

        /// <summary>
        /// Busqueda por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/inscripciones/{id}")]
        public IActionResult getbyId(int id)
        {
            var unaInscripciones = from e in _contexto.inscripciones
                                join alum in _contexto.alumnos on e.alumnoId equals alum.id
                                join mat in _contexto.materias on e.materiaId equals mat.id
                                where e.id == id //Filtro por ID
                                select new
                                {
                                    e.id,
                                    alum.nombre,
                                    mat.materia,
                                    e.inscripcion,
                                    e.fecha,
                                    e.estado
                                };
            if (unaInscripciones != null)
            {
                return Ok(unaInscripciones);
            }
            return NotFound();
        }
    }
}
