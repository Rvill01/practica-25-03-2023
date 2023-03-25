using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica_25_03_2023.Models;

namespace practica_25_03_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly universidadContext _universidadContext;
        public equiposController(universidadContext universidadContext)
        {
            _universidadContext = universidadContext;

        }

        [HttpGet]
        [Route("GetAllCarreras")]
        public IActionResult Get()
        {
            var listadoCarreras = (from c in _universidadContext.carreras
                                   join f in _universidadContext.facultades
                                   on c.facultad_id equals f.facultad_id
                                   select new
                                   {
                                       c.carrera_id,
                                       c.nombre_carrera,
                                       f.nombre_facultad
                                   }).ToList();


            if (listadoCarreras.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoCarreras);
        }

        [HttpPost]
        [Route("AddCarrera")]
        public IActionResult CreatMotorista([FromBody] carreras carrera)
        {
            try
            {
                _universidadContext.carreras.Add(carrera);
                _universidadContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updateCarrera/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] carreras carreraModificar)
        {
            carreras? carreraActual = (from e in _universidadContext.carreras
                                       where e.carrera_id == id
                                       select e).FirstOrDefault();
            if (carreraActual == null)
            {
                return NotFound();
            }

            carreraActual.nombre_carrera = carreraModificar.nombre_carrera;
            carreraActual.facultad_id = carreraModificar.facultad_id;


            _universidadContext.Entry(carreraActual).State = EntityState.Modified;
            _universidadContext.SaveChanges();

            return Ok(carreraModificar);

        }

        [HttpDelete]
        [Route("eliminarCarrera/{id}")]
        public IActionResult Eliminarcarrera(int id)
        {
            carreras? carrera = (from e in _universidadContext.carreras
                                 where e.carrera_id == id
                                 select e).FirstOrDefault();
            if (carrera == null)
                return NotFound();

            _universidadContext.carreras.Attach(carrera);
            _universidadContext.carreras.Remove(carrera);
            _universidadContext.SaveChanges();

            return Ok(carrera);
        }

    }
}
