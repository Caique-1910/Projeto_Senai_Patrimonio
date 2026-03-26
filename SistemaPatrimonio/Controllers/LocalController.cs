using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaPatrimonio.Applications.Services;
using SistemaPatrimonio.DTOs.LocalDto;
using SistemaPatrimonio.Exceptions;

namespace SistemaPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {
        private readonly LocalService _service;

        public LocalController(LocalService service) 
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarLocalDto>> Listar() 
        {
            List<ListarLocalDto> locais = _service.Listar();

            return Ok(locais);
        }

        [HttpGet("{id}")]
        public ActionResult BuscarPorId(Guid id) 
        {
            try
            {
                ListarLocalDto local = _service.BuscarPorId(id);
                return Ok(local);
            }
            catch (DomainException ex) 
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarLocalDto dto) 
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
        public ActionResult Atualizar(Guid id, CriarLocalDto dto) 
        {
            try
            {
                _service.Atualizar(id,dto);
                return NoContent();
                
            }
            catch (DomainException ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
