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

        /// <summary>
        /// Insertar inscripciones
        /// </summary>
        /// <param name="inscripcionesNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/inscripciones")]
        public IActionResult guardarInscripciones([FromBody] inscripciones inscripcionesNuevo)
        {
            try
            {
                IEnumerable<inscripciones> inscripcionesExiste = from e in _contexto.inscripciones
                                                      where e.materiaId == inscripcionesNuevo.materiaId
                                                      && e.alumnoId == inscripcionesNuevo.alumnoId
                                                      select e;
                if (inscripcionesExiste.Count() == 0)
                {
                    _contexto.inscripciones.Add(inscripcionesNuevo);
                    _contexto.SaveChanges();
                    return Ok(inscripcionesNuevo);
                }
                return Ok(inscripcionesExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Modificar inscripciones
        /// </summary>
        /// <param name="inscripcionesAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/inscripciones")]
        public IActionResult updateInscripciones([FromBody] inscripciones inscripcionesAModificar)
        {
            inscripciones inscripcionesExiste = (from e in _contexto.inscripciones
                                     where e.id == inscripcionesAModificar.id
                                     select e).FirstOrDefault();
            if (inscripcionesExiste is null)
            {
                return NotFound();
            }
            inscripcionesExiste.alumnoId = inscripcionesAModificar.alumnoId;
            inscripcionesExiste.materiaId = inscripcionesAModificar.materiaId;
            inscripcionesExiste.inscripcion = inscripcionesAModificar.inscripcion;
            inscripcionesExiste.fecha = inscripcionesAModificar.fecha;
            inscripcionesExiste.estado = inscripcionesAModificar.estado;

            _contexto.Entry(inscripcionesExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(inscripcionesExiste);
        }
    }
}
