using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2017AS603_2016AR602.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2017AS603_2016AR602.Controllers
{
    public class alumnosController : Controller
    {
        private readonly _2017AS603_2016AR602Context _contexto;
        public alumnosController(_2017AS603_2016AR602Context miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Busqueda en general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/alumnos")]
        public IActionResult Get()
        {
            var alumnos = from e in _contexto.alumnos
                          join depart in _contexto.departamentos on e.departamentoId equals depart.id
                           select new
                           {
                               e.id,
                               e.carnet,
                               e.nombre,
                               e.apellidos,
                               e.dui,
                               depart.departamento,
                               e.estado
                           };
            if (alumnos.Count() > 0)
            {
                return Ok(alumnos);
            }
            return NotFound();
        }

        /// <summary>
        /// Busqueda por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/alumnos/{id}")]
        public IActionResult getbyId(int id)
        {
            var unAlumnos = from e in _contexto.alumnos
                          join depart in _contexto.departamentos on e.departamentoId equals depart.id
                          where e.id == id //Filtro por ID
                          select new
                          {
                              e.id,
                              e.carnet,
                              e.nombre,
                              e.apellidos,
                              e.dui,
                              depart.departamento,
                              e.estado
                          };
            if (unAlumnos != null)
            {
                return Ok(unAlumnos);
            }
            return NotFound();
        }

        /// <summary>
        /// Insertar materias
        /// </summary>
        /// <param name="alumnoNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/alumnos")]
        public IActionResult guardarMaterias([FromBody] alumnos  alumnoNuevo)
        {
            try
            {
                IEnumerable<alumnos> materiasExiste = from e in _contexto.alumnos
                                                      where e.dui == alumnoNuevo.dui
                                                      || e.carnet == alumnoNuevo.carnet
                                                      select e;
                if (materiasExiste.Count() == 0)
                {
                    _contexto.alumnos.Add(alumnoNuevo);
                    _contexto.SaveChanges();
                    return Ok(alumnoNuevo);
                }
                return Ok(materiasExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Modificar alumnos
        /// </summary>
        /// <param name="alumnosAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/alumnos")]
        public IActionResult updateAlumno([FromBody] alumnos alumnosAModificar)
        {
            alumnos materiaExiste = (from e in _contexto.alumnos
                                      where e.id == alumnosAModificar.id
                                      select e).FirstOrDefault();
            if (materiaExiste is null)
            {
                return NotFound();
            }
            materiaExiste.carnet = alumnosAModificar.carnet;
            materiaExiste.nombre = alumnosAModificar.nombre;
            materiaExiste.apellidos = alumnosAModificar.apellidos;
            materiaExiste.dui = alumnosAModificar.dui;
            materiaExiste.departamentoId = alumnosAModificar.departamentoId;
            materiaExiste.estado = alumnosAModificar.estado;

            _contexto.Entry(materiaExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(materiaExiste);
        }


        /// <summary>
        /// Busqueda por departament
        /// </summary>
        /// <param name="buscarNombre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/materias/buscarPorDepa/{buscarPorDepa}")]
        public IActionResult obtenerNombre(string buscarPorDepa)
        {
            var alumnoPorNombre = from e in _contexto.alumnos
                          join depart in _contexto.departamentos on e.departamentoId equals depart.id
                          where depart.departamento.Contains(buscarPorDepa)
                          select new
                          {
                              e.id,
                              e.carnet,
                              e.nombre,
                              e.apellidos,
                              e.dui,
                              depart.departamento,
                              e.estado
                          };
            if (alumnoPorNombre.Count() > 0)
            {
                return Ok(alumnoPorNombre);
            }
            return NotFound();
        }
    }
}
