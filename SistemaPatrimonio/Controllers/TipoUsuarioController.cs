using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaPatrimonio.Applications.Services;
using SistemaPatrimonio.DTOs.TipoUsuarioDto;
using SistemaPatrimonio.Exceptions;

namespace SistemaPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly TipoUsuarioService _service;

        public TipoUsuarioController(TipoUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public ActionResult BuscarPorId(Guid id)
        {
            try
            {
                ListarTipoUsuarioDto tpU = _service.BuscarPorId(id);
                return Ok(tpU);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoUsuarioDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarTipoUsuarioDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
