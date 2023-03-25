using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica_25_03_2023.Models;

namespace practica_25_03_2023.Controllers
{
    public class usuarioController:ControllerBase
    {
        private readonly universidadContext _universidadContext;
        public usuarioController(universidadContext universidadContext)
        {
            _universidadContext = universidadContext;

        }

        [HttpGet]
        [Route("GetAllUsuarios")]
        public IActionResult Get()
        {
            var listadoUsuarios = (from u in _universidadContext.usuarios
                                   join c in _universidadContext.carreras
                                   on u.carrera_id equals c.carrera_id
                                   select new
                                   {
                                       u.usuario_id,
                                       u.nombre,
                                       u.documento,
                                       u.tipo,
                                       u.carnet,
                                       c.nombre_carrera
                                   }).ToList();
            if (listadoUsuarios.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoUsuarios);

        }

        [HttpPost]
        [Route("AddUsuarios")]
        public IActionResult CreatMotorista([FromBody] usuarios usuario)
        {
            try
            {
                _universidadContext.usuarios.Add(usuario);
                _universidadContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updateUsuario/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] usuarios usuarioModificar)
        {
            usuarios? usuarioActual = (from e in _universidadContext.usuarios
                                       where e.usuario_id == id
                                       select e).FirstOrDefault();
            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.nombre = usuarioModificar.nombre;
            usuarioActual.documento = usuarioModificar.documento;
            usuarioActual.tipo = usuarioModificar.tipo;
            usuarioActual.carnet = usuarioModificar.carnet;
            usuarioActual.carrera_id = usuarioModificar.carrera_id;


            _universidadContext.Entry(usuarioActual).State = EntityState.Modified;
            _universidadContext.SaveChanges();

            return Ok(usuarioModificar);

        }

        [HttpDelete]
        [Route("eliminarUsuario/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            usuarios? usuario = (from e in _universidadContext.usuarios
                                 where e.usuario_id == id
                                 select e).FirstOrDefault();
            if (usuario == null)
                return NotFound();

            _universidadContext.usuarios.Attach(usuario);
            _universidadContext.usuarios.Remove(usuario);
            _universidadContext.SaveChanges();

            return Ok(usuario);
        }

    }
}
