using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2017AS603_2016AR602.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2017AS603_2016AR602.Controllers
{
    public class facultadController : Controller
    {
        private readonly _2017AS603_2016AR602Context _contexto;
        public facultadController(_2017AS603_2016AR602Context miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Busqueda en general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/facultad")]
        public IActionResult Get()
        {
            var facultads = from e in _contexto.facultad
                               select new
                               {
                                   e.id,
                                   e.faculta,
                               };
            if (facultads.Count() > 0)
            {
                return Ok(facultads);
            }
            return NotFound();
        }

        /// <summary>
        /// Busqueda por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/facultad/{id}")]
        public IActionResult getbyId(int id)
        {
            var unaFacultad = from e in _contexto.facultad
                             where e.id == id //Filtro por ID
                             select new
                             {
                                 e.id,
                                 e.faculta,
                             };

            if (unaFacultad != null)
            {
                return Ok(unaFacultad);
            }
            return NotFound();
        }

        /// <summary>
        /// Insertar facultad
        /// </summary>
        /// <param name="facultadNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/facultad")]
        public IActionResult guardarFacultad([FromBody] facultad facultadNuevo)
        {
            try
            {
                IEnumerable<facultad> facultadExiste = from e in _contexto.facultad
                                                           where e.faculta == facultadNuevo.faculta
                                                           select e;
                if (facultadExiste.Count() == 0)
                {
                    _contexto.facultad.Add(facultadNuevo);
                    _contexto.SaveChanges();
                    return Ok(facultadNuevo);
                }
                return Ok(facultadExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Modificar departamento
        /// </summary>
        /// <param name="facultadAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/facultad")]
        public IActionResult updateFacultad([FromBody] facultad facultadAModificar)
        {
            facultad facultadExiste = (from e in _contexto.facultad
                                                where e.id == facultadAModificar.id
                                                select e).FirstOrDefault();
            if (facultadExiste is null)
            {
                return NotFound();
            }
            facultadExiste.faculta = facultadAModificar.faculta;

            _contexto.Entry(facultadExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(facultadExiste);

        }
    }
}
