using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaPatrimonio.Applications.Services;
using SistemaPatrimonio.DTOs.StatusTransferenciaDto;
using SistemaPatrimonio.Exceptions;

namespace SistemaPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTransferenciaController : ControllerBase
    {
        private readonly StatusTransferenciaService _service;

        public StatusTransferenciaController(StatusTransferenciaService service)
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
                ListarStatusTransferenciaDto stDto = _service.BuscarPorId(id);
                return Ok(stDto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarStatusTransferenciaDto dto)
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
        public ActionResult Atualizar(Guid id, CriarStatusTransferenciaDto dto)
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
