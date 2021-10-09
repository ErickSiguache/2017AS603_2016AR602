using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2017AS603_2016AR602.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2017AS603_2016AR602.Controllers
{
    public class materiasController : Controller
    {
        private readonly _2017AS603_2016AR602Context _contexto;
        public materiasController(_2017AS603_2016AR602Context miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Busqueda en general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/materias")]
        public IActionResult Get()
        {
            var materias = from e in _contexto.materias
                           join facul in _contexto.facultad on e.facultadId equals facul.id
                            select new
                            {
                                e.id,
                                facul.faculta,
                                e.materia,
                                e.unidades_valorativas,
                                e.estado
                            };
            if (materias.Count() > 0)
            {
                return Ok(materias);
            }
            return NotFound();
        }

        /// <summary>
        /// Busqueda por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/materias/{id}")]
        public IActionResult getbyId(int id)
        {
            var unaMaterias = from e in _contexto.materias
                           join facul in _contexto.facultad on e.facultadId equals facul.id
                           where e.id == id //Filtro por ID
                           select new
                           {
                               e.id,
                               facul.faculta,
                               e.materia,
                               e.unidades_valorativas,
                               e.estado
                           };
            if (unaMaterias != null)
            {
                return Ok(unaMaterias);
            }
            return NotFound();
        }

        /// <summary>
        /// Insertar materias
        /// </summary>
        /// <param name="materiasNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/materias")]
        public IActionResult guardarMaterias([FromBody] materias materiasNuevo)
        {
            try
            {
                IEnumerable<materias> materiasExiste = from e in _contexto.materias
                                                       where e.materia == materiasNuevo.materia
                                                       select e;
                if (materiasExiste.Count() == 0)
                {
                    _contexto.materias.Add(materiasNuevo);
                    _contexto.SaveChanges();
                    return Ok(materiasNuevo);
                }
                return Ok(materiasExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Modificar materia
        /// </summary>
        /// <param name="materiaAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/materias")]
        public IActionResult updateMateria([FromBody] materias materiaAModificar)
        {
            materias materiaExiste = (from e in _contexto.materias
                                       where e.id == materiaAModificar.id
                                       select e).FirstOrDefault();
            if (materiaExiste is null)
            {
                return NotFound();
            }
            materiaExiste.facultadId = materiaAModificar.facultadId;
            materiaExiste.materia = materiaAModificar.materia;
            materiaExiste.unidades_valorativas = materiaAModificar.unidades_valorativas;
            materiaExiste.estado = materiaAModificar.estado;

            _contexto.Entry(materiaExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(materiaExiste);
        }

        /// <summary>
        /// Busqueda por facultad
        /// </summary>
        /// <param name="buscarNombre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/materias/buscarnombre/{buscarNombre}")]
        public IActionResult obtenerNombre(string buscarNombre)
        {
            var materiaPorNombre = from e in _contexto.materias
                              join facul in _contexto.facultad on e.facultadId equals facul.id
                              where facul.faculta.Contains(buscarNombre)
                              select new
                              {
                                  e.id,
                                  facul.faculta,
                                  e.materia,
                                  e.unidades_valorativas,
                                  e.estado
                              };
            if (materiaPorNombre.Count() > 0)
            {
                return Ok(materiaPorNombre);
            }
            return NotFound();
        }
    }
}
