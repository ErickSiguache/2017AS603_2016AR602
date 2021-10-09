using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2017AS603_2016AR602.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2017AS603_2016AR602.Controllers
{
    public class notasController : Controller
    {
        private readonly _2017AS603_2016AR602Context _contexto;
        public notasController(_2017AS603_2016AR602Context miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Busqueda en general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/notas")]
        public IActionResult Get()
        {
            var notas = from e in _contexto.notas
                        join inscri in _contexto.inscripciones on e.inscripcionId equals inscri.id
                               select new
                               {
                                   e.id,
                                   inscri.inscripcion,
                                   e.evaluacion,
                                   e.nota,
                                   e.porcentaje,
                                   e.fecha
                               };
            if (notas.Count() > 0)
            {
                return Ok(notas);
            }
            return NotFound();
        }

        /// <summary>
        /// Busqueda por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/notas/{id}")]
        public IActionResult getbyId(int id)
        {
            var unaNotas = from e in _contexto.notas
                                   join inscri in _contexto.inscripciones on e.inscripcionId equals inscri.id
                                   where e.id == id //Filtro por ID
                                   select new
                                   {
                                       e.id,
                                       inscri.inscripcion,
                                       e.evaluacion,
                                       e.nota,
                                       e.porcentaje,
                                       e.fecha
                                   };
            if (unaNotas != null)
            {
                return Ok(unaNotas);
            }
            return NotFound();
        }

        /// <summary>
        /// Insertar inscripciones
        /// </summary>
        /// <param name="notasNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/notas")]
        public IActionResult guardarNotas([FromBody] notas notasNuevo)
        {
            try
            {
                IEnumerable<notas> notasExiste = from e in _contexto.notas
                                                                 where e.evaluacion == notasNuevo.evaluacion
                                                                 && e.inscripcionId == notasNuevo.inscripcionId
                                                                 select e;
                if (notasExiste.Count() == 0)
                {
                    _contexto.notas.Add(notasNuevo);
                    _contexto.SaveChanges();
                    return Ok(notasNuevo);
                }
                return Ok(notasExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Modificar inscripciones
        /// </summary>
        /// <param name="notasAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/notas")]
        public IActionResult updateNotas([FromBody] notas notasAModificar)
        {
            notas notasExiste = (from e in _contexto.notas
                                                 where e.id == notasAModificar.id
                                                 select e).FirstOrDefault();
            if (notasExiste is null)
            {
                return NotFound();
            }
            notasExiste.inscripcionId = notasAModificar.inscripcionId;
            notasExiste.evaluacion = notasAModificar.evaluacion;
            notasExiste.nota = notasAModificar.nota;
            notasExiste.porcentaje = notasAModificar.porcentaje;
            notasExiste.fecha = notasAModificar.fecha;

            _contexto.Entry(notasExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(notasExiste);
        }
    }
}
