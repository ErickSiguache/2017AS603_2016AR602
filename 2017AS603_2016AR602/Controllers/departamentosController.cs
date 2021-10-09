using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2017AS603_2016AR602.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2017AS603_2016AR602.Controllers
{
    public class departamentosController : Controller
    {
        private readonly _2017AS603_2016AR602Context _contexto;
        public departamentosController(_2017AS603_2016AR602Context miContexto)
        {
            this._contexto = miContexto;
        }
        

        /// <summary>
        /// Busqueda en general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/departamento")]
        public IActionResult Get()
        {
            var departamento = from e in _contexto.departamentos
                           select new
                           {
                               e.id,
                               e.departamento,
                           };
            if (departamento.Count() > 0)
            {
                return Ok(departamento);
            }
            return NotFound();
        }

        /// <summary>
        /// Busqueda por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/departamento/{id}")]
        public IActionResult getbyId(int id)
        {
            var unaCarrera = from e in _contexto.departamentos
                             where e.id == id //Filtro por ID
                             select new
                             {
                                 e.id,
                                 e.departamento,
                             };

            if (unaCarrera != null)
            {
                return Ok(unaCarrera);
            }
            return NotFound();
        }


        /// <summary>
        /// Insertar Departamento
        /// </summary>
        /// <param name="departamentoNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/departamento")]
        public IActionResult guardarDepartamento([FromBody] departamentos departamentoNuevo)
        {
            try
            {
                IEnumerable<departamentos> departamentoExiste = from e in _contexto.departamentos
                                                      where e.departamento == departamentoNuevo.departamento
                                                      select e;
                if (departamentoExiste.Count() == 0)
                {
                    _contexto.departamentos.Add(departamentoNuevo);
                    _contexto.SaveChanges();
                    return Ok(departamentoNuevo);
                }
                return Ok(departamentoExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Modificar departamento
        /// </summary>
        /// <param name="departamentoAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/carreras")]
        public IActionResult updateCarrera([FromBody] departamentos departamentoAModificar)
        {
            departamentos departamentoExiste = (from e in _contexto.departamentos
                                      where e.id == departamentoAModificar.id
                                      select e).FirstOrDefault();
            if (departamentoExiste is null)
            {
                return NotFound();
            }
            departamentoExiste.departamento = departamentoAModificar.departamento;

            _contexto.Entry(departamentoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(departamentoExiste);

        }
    }
}
