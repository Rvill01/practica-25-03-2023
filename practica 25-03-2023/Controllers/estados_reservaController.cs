using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica_25_03_2023.Models;

namespace practica_25_03_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estados_reservaController : ControllerBase
    {
        private readonly universidadContext _universidadContext;
        public estados_reservaController(universidadContext universidadContext)
        {
            _universidadContext = universidadContext;

        }

        [HttpGet]
        [Route("GetAllEstadosReserva")]
        public IActionResult Get()
        {
            List<estados_reserva> listadoEstadosReserva = (from e in _universidadContext.estados_reserva
                                                           select e).ToList();
            if (listadoEstadosReserva.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEstadosReserva);
        }

        [HttpPost]
        [Route("AddEstadoReserva")]
        public IActionResult CreatMotorista([FromBody] estados_reserva estadoReserva)
        {
            try
            {
                _universidadContext.estados_reserva.Add(estadoReserva);
                _universidadContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updateEstadoReserva/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] estados_reserva estadoReservaModificar)
        {
            estados_reserva? estadoReservaActual = (from e in _universidadContext.estados_reserva
                                                    where e.estado_res_id == id
                                                    select e).FirstOrDefault();
            if (estadoReservaActual == null)
            {
                return NotFound();
            }

            estadoReservaActual.estado = estadoReservaModificar.estado;


            _universidadContext.Entry(estadoReservaActual).State = EntityState.Modified;
            _universidadContext.SaveChanges();

            return Ok(estadoReservaModificar);

        }

        [HttpDelete]
        [Route("eliminarEstadoReserva/{id}")]
        public IActionResult EliminarEstadoReserca(int id)
        {
            estados_reserva? estadoReserva = (from e in _universidadContext.estados_reserva
                                              where e.estado_res_id == id
                                              select e).FirstOrDefault();
            if (estadoReserva == null)
                return NotFound();

            _universidadContext.estados_reserva.Attach(estadoReserva);
            _universidadContext.estados_reserva.Remove(estadoReserva);
            _universidadContext.SaveChanges();

            return Ok(estadoReserva);
        }
    }
}
