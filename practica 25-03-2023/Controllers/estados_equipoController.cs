using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica_25_03_2023.Models;

namespace practica_25_03_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estados_equipoController: ControllerBase
    {
        private readonly universidadContext _universidadContext;
        public estados_equipoController(universidadContext universidadContext)
        {
            _universidadContext = universidadContext;

        }

        [HttpGet]
        [Route("GetAllEstadosEquipos")]
        public IActionResult Get()
        {
            List<estados_equipo> listadoEstadosEquipos = (from e in _universidadContext.estados_equipo
                                                          select e).ToList();
            if (listadoEstadosEquipos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEstadosEquipos);
        }

        [HttpPost]
        [Route("AddEstadoEquipo")]
        public IActionResult CreatMotorista([FromBody] estados_equipo estadoEquipo)
        {
            try
            {
                _universidadContext.estados_equipo.Add(estadoEquipo);
                _universidadContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updateEstadoEquipo/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] estados_equipo estadoEquipoModificar)
        {
            estados_equipo? estadoEquipoActual = (from e in _universidadContext.estados_equipo
                                                  where e.id_estados_equipo == id
                                                  select e).FirstOrDefault();
            if (estadoEquipoActual == null)
            {
                return NotFound();
            }

            estadoEquipoActual.descripcion = estadoEquipoModificar.descripcion;
            estadoEquipoActual.estado = estadoEquipoModificar.estado;


            _universidadContext.Entry(estadoEquipoActual).State = EntityState.Modified;
            _universidadContext.SaveChanges();

            return Ok(estadoEquipoModificar);

        }

        [HttpDelete]
        [Route("eliminarEstadoEquipo/{id}")]
        public IActionResult EliminarEstadoEquipo(int id)
        {
            estados_equipo? estadoEquipo = (from e in _universidadContext.estados_equipo
                                            where e.id_estados_equipo == id
                                            select e).FirstOrDefault();
            if (estadoEquipo == null)
                return NotFound();

            _universidadContext.estados_equipo.Attach(estadoEquipo);
            _universidadContext.estados_equipo.Remove(estadoEquipo);
            _universidadContext.SaveChanges();

            return Ok(estadoEquipo);
        }

    }
}
