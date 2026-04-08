using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaPatrimonio.Applications.Services;
using SistemaPatrimonio.DTOs.SolicitacaoTransferenciaDto;
using SistemaPatrimonio.Exceptions;

namespace SistemaPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoTransferenciaController : ControllerBase
    {
        private readonly SolicitacaoTransferenciaService _service;

        public SolicitacaoTransferenciaController(SolicitacaoTransferenciaService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult <List<ListarSolicitacaoTransferenciaDto>> Listar()
        {
            List<ListarSolicitacaoTransferenciaDto> solicitacoes = _service.Listar();
            return Ok(solicitacoes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarSolicitacaoTransferenciaDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarSolicitacaoTransferenciaDto solicitacao = _service.BuscarPorId(id);
                return Ok(solicitacao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
