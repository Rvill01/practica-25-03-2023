using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica_25_03_2023.Models;

namespace practica_25_03_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class facultadesController:ControllerBase
    {
        private readonly universidadContext _universidadContext;
        public facultadesController(universidadContext universidadContext)
        {
            _universidadContext = universidadContext;

        }

        [HttpGet]
        [Route("GetAllFacultades")]
        public IActionResult Get()
        {
            List<facultades> listadoFacultades = (from e in _universidadContext.facultades
                                                  select e).ToList();
            if (listadoFacultades.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoFacultades);
        }

        [HttpPost]
        [Route("AddFacultad")]
        public IActionResult CreatMotorista([FromBody] facultades facultad)
        {
            try
            {
                _universidadContext.facultades.Add(facultad);
                _universidadContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updatefacultad/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] facultades facultadModificar)
        {
            facultades? facultadActual = (from e in _universidadContext.facultades
                                          where e.facultad_id == id
                                          select e).FirstOrDefault();
            if (facultadActual == null)
            {
                return NotFound();
            }

            facultadActual.nombre_facultad = facultadModificar.nombre_facultad;


            _universidadContext.Entry(facultadActual).State = EntityState.Modified;
            _universidadContext.SaveChanges();

            return Ok(facultadModificar);

        }

        [HttpDelete]
        [Route("eliminarFacultad/{id}")]
        public IActionResult Eliminarfacultad(int id)
        {
            facultades? facultad = (from e in _universidadContext.facultades
                                    where e.facultad_id == id
                                    select e).FirstOrDefault();
            if (facultad == null)
                return NotFound();

            _universidadContext.facultades.Attach(facultad);
            _universidadContext.facultades.Remove(facultad);
            _universidadContext.SaveChanges();

            return Ok(facultad);
        }
    }
}
