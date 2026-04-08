using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.SolicitacaoTransferenciaDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public List<ListarSolicitacaoTransferenciaDto> Listar() 
        {
            List<SolicitacaoTransferencia> soliitacoes = _repository.Listar();

            List<ListarSolicitacaoTransferenciaDto> solicitacaoDtos = soliitacoes.Select(s => new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = s.TransferenciaID,
                DataCriacaoSolicitacao = s.DataCriacaoSolicitante,
                DataResposta = s.DataResposta,
                Justificativa = s.Justificativa,
                StatusTransferenciaID = s.StatusTransferenciaID,
                UsuarioIDSolicitacao = s.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = s.UsuarioIDAprovacao,
                PatrimonioID = s.PatrimonioID,
                LocalID = s.LocalID
            }).ToList();

            return solicitacaoDtos;
        }

        public ListarSolicitacaoTransferenciaDto BuscarPorId(Guid id)
        {
            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(id);
            
            if(solicitacao == null)
            {
                throw new DomainException("Solicitação não encontrada.");
            }

            ListarSolicitacaoTransferenciaDto solicitacaoDto = new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = solicitacao.TransferenciaID,
                DataCriacaoSolicitacao = solicitacao.DataCriacaoSolicitante,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaID = solicitacao.StatusTransferenciaID,
                UsuarioIDSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioID = solicitacao.PatrimonioID,
                LocalID = solicitacao.LocalID
            };

            return solicitacaoDto;
        }
    }
}
