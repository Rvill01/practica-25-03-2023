using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica_25_03_2023.Models;

namespace practica_25_03_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipo_equipoController : ControllerBase
    {
        private readonly universidadContext _universidadContext;
        public tipo_equipoController(universidadContext universidadContext)
        {
            _universidadContext = universidadContext;

        }

        [HttpGet]
        [Route("GetAllTipoEquipos")]
        public IActionResult Get()
        {
            List<tipo_equipo> listadoTipoEquipos = (from e in _universidadContext.tipos_equipo
                                                    select e).ToList();
            if (listadoTipoEquipos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoTipoEquipos);
        }

        [HttpPost]
        [Route("AddTipoEquipo")]
        public IActionResult CreatMotorista([FromBody] tipo_equipo tipoEquipo)
        {
            try
            {
                _universidadContext.tipos_equipo.Add(tipoEquipo);
                _universidadContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updateTipoEquipo/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] tipo_equipo tipoEquipoModificar)
        {
            tipo_equipo? tipoEquipoActual = (from e in _universidadContext.tipos_equipo
                                             where e.id_tipo_equipo == id
                                             select e).FirstOrDefault();
            if (tipoEquipoActual == null)
            {
                return NotFound();
            }

            tipoEquipoActual.descripcion = tipoEquipoModificar.descripcion;
            tipoEquipoActual.estado = tipoEquipoModificar.estado;


            _universidadContext.Entry(tipoEquipoActual).State = EntityState.Modified;
            _universidadContext.SaveChanges();

            return Ok(tipoEquipoModificar);

        }

        [HttpDelete]
        [Route("eliminarTipoEquipo/{id}")]
        public IActionResult EliminartipoEquipo(int id)
        {
            tipo_equipo? tipoEquipo = (from e in _universidadContext.tipos_equipo
                                       where e.id_tipo_equipo == id
                                       select e).FirstOrDefault();
            if (tipoEquipo == null)
                return NotFound();

            _universidadContext.tipos_equipo.Attach(tipoEquipo);
            _universidadContext.tipos_equipo.Remove(tipoEquipo);
            _universidadContext.SaveChanges();

            return Ok(tipoEquipo);
        }
    }
}
