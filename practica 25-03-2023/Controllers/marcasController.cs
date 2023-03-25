using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica_25_03_2023.Models;

namespace practica_25_03_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController:ControllerBase
    {
        private readonly universidadContext _universidadContext;
        public marcasController(universidadContext universidadContext)
        {
            _universidadContext = universidadContext;

        }

        [HttpGet]
        [Route("GetAllMarcas")]
        public IActionResult Get()
        {
            List<marcas> listadoMarcas = (from e in _universidadContext.marcas
                                          select e).ToList();
            if (listadoMarcas.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoMarcas);
        }

        [HttpPost]
        [Route("AddMarca")]
        public IActionResult CreatMotorista([FromBody] marcas marca)
        {
            try
            {
                _universidadContext.marcas.Add(marca);
                _universidadContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updatemarca/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] marcas marcaModificar)
        {
            marcas? marcaActual = (from e in _universidadContext.marcas
                                   where e.id_marcas == id
                                   select e).FirstOrDefault();
            if (marcaActual == null)
            {
                return NotFound();
            }

            marcaActual.nombre_marca = marcaModificar.nombre_marca;
            marcaActual.estados = marcaModificar.estados;


            _universidadContext.Entry(marcaActual).State = EntityState.Modified;
            _universidadContext.SaveChanges();

            return Ok(marcaModificar);

        }

        [HttpDelete]
        [Route("eliminarmarca/{id}")]
        public IActionResult Eliminarmarca(int id)
        {
            marcas? marca = (from e in _universidadContext.marcas
                             where e.id_marcas == id
                             select e).FirstOrDefault();
            if (marca == null)
                return NotFound();

            _universidadContext.marcas.Attach(marca);
            _universidadContext.marcas.Remove(marca);
            _universidadContext.SaveChanges();

            return Ok(marca);
        }
    }
}
