using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaPatrimonio.Applications.Services;
using SistemaPatrimonio.DTOs.SolicitacaoTransferenciaDto;
using SistemaPatrimonio.Exceptions;
using System.Security.Claims;

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

        [Authorize]
        [HttpPost]
        public ActionResult Adicionar(CriarSolicitacaoTransferenciaDto dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if(string.IsNullOrWhiteSpace(usuarioIdClaim))
                {
                    return Unauthorized("Usuario não autenticado");
                }

                Guid id = Guid.Parse(usuarioIdClaim);

                _service.Adicionar(id, dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("{id}/responder")]
        public ActionResult Responder(Guid id, ResponderSolicitacaoTransferenciaDto dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(usuarioIdClaim))
                {
                    return Unauthorized("Usuario não autenticado");
                }

                Guid usuarioid = Guid.Parse(usuarioIdClaim);

                _service.Responder(id, usuarioid, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
