using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaPatrimonio.Applications.Services;
using SistemaPatrimonio.DTOs.TipoAlteracaoDto;
using SistemaPatrimonio.DTOs.TipoUsuarioDto;
using SistemaPatrimonio.Exceptions;

namespace SistemaPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAlteracaoController : ControllerBase
    {
        private readonly TipoAlteracaoService _service;

        public TipoAlteracaoController(TipoAlteracaoService service)
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
                ListarTipoAlteracaoDto tpa = _service.BuscarPorId(id);
                return Ok(tpa);
            }
            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoAlteracaoDto dto)
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
        public ActionResult Atualizar(Guid id, CriarTipoAlteracaoDto dto)
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
