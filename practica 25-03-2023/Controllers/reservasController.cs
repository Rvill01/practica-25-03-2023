using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica_25_03_2023.Models;

namespace practica_25_03_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class reservasController: ControllerBase
    {
        private readonly universidadContext _universidadContext;
        public reservasController(universidadContext universidadContext)
        {
            _universidadContext = universidadContext;

        }

        [HttpGet]
        [Route("GetAllReservas")]
        public IActionResult Get()
        {
            var listadoReservas = (from r in _universidadContext.reservas
                                   join e in _universidadContext.equipos on r.equipo_id equals e.id_equipos
                                   join u in _universidadContext.usuarios on r.usuario_id equals u.usuario_id
                                   join re in _universidadContext.estados_reserva on r.estado_reserva_id equals re.estado_res_id
                                   select new
                                   {
                                       r.reserva_id,
                                       nombreEquipo = e.nombre,
                                       nombreUsuario = u.nombre,
                                       r.hora_salida,
                                       r.hora_retorno,
                                       r.tiempo_reserva,
                                       re.estado

                                   }).ToList();

            if (listadoReservas.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoReservas);
        }

        [HttpPost]
        [Route("AddReserva")]
        public IActionResult CreatMotorista([FromBody] reservas reserva)
        {
            try
            {
                _universidadContext.reservas.Add(reserva);
                _universidadContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updatereserva/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] reservas reservaModificar)
        {
            reservas? reservaActual = (from e in _universidadContext.reservas
                                       where e.reserva_id == id
                                       select e).FirstOrDefault();
            if (reservaActual == null)
            {
                return NotFound();
            }

            reservaActual.equipo_id = reservaModificar.equipo_id;
            reservaActual.usuario_id = reservaModificar.usuario_id;
            reservaActual.fecha_salida = reservaModificar.fecha_salida;
            reservaActual.hora_salida = reservaModificar.hora_salida;
            reservaActual.tiempo_reserva = reservaModificar.tiempo_reserva;
            reservaActual.estado_reserva_id = reservaModificar.estado_reserva_id;
            reservaActual.fecha_retorno = reservaActual.fecha_retorno;
            reservaActual.hora_retorno = reservaActual.hora_retorno;


            _universidadContext.Entry(reservaActual).State = EntityState.Modified;
            _universidadContext.SaveChanges();

            return Ok(reservaModificar);

        }

        [HttpDelete]
        [Route("eliminarReserva/{id}")]
        public IActionResult Eliminarreserva(int id)
        {
            reservas? reserva = (from e in _universidadContext.reservas
                                 where e.reserva_id == id
                                 select e).FirstOrDefault();
            if (reserva == null)
                return NotFound();

            _universidadContext.reservas.Attach(reserva);
            _universidadContext.reservas.Remove(reserva);
            _universidadContext.SaveChanges();

            return Ok(reserva);
        }
    }
}
