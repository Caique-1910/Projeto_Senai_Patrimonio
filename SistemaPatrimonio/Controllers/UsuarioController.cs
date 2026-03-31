using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaPatrimonio.Applications.Services;
using SistemaPatrimonio.DTOs.UsuarioDto;

namespace SistemaPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarUsuarioDto>> Listar()
        {
            List<ListarUsuarioDto> usuarios = _service.Listar();
            return Ok(usuarios);
        }
    }
}
