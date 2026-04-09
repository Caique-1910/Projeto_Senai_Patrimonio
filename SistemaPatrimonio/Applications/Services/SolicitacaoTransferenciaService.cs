using SistemaPatrimonio.Applications.Regras;
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

        public void Adicionar(Guid usuarioId, CriarSolicitacaoTransferenciaDto dto)
        {
            Validar.ValidarJustificativa(dto.Justificativa);

            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(dto.PatrimonioID);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if(!_repository.LocalExiste(dto.LocalID))
            {
                throw new DomainException("Local de destino não existe.");
            }

            if(patrimonio.LocalID == dto.LocalID)
            {
                throw new DomainException("O patrimônio já está no local de destino.");
            }

            if(_repository.ExisteSolicitacaoPendente(dto.PatrimonioID))
            {
                throw new DomainException("Já existe uma solicitação de transferência pendente para este patrimônio.");
            }

            if(usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDoLocal(usuario.UsuarioID, patrimonio.LocalID);

                if (!usuarioResponsavel)
                {
                    throw new DomainException("O responsável só pode solicitar transferencia dde patrimonio do ambiente ao qual está vinculado.");
                }
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if(statusPendente == null)
            {
                throw new DomainException("Status de transferência pendente não encontrado.");
            }

            SolicitacaoTransferencia solicitacao = new SolicitacaoTransferencia
            {
                DataCriacaoSolicitante = DateTime.Now,
                Justificativa = dto.Justificativa,
                StatusTransferenciaID = statusPendente.StatusTransferenciaID,
                UsuarioIDAprovacao = null,
                UsuarioIDSolicitacao = usuarioId,
                PatrimonioID = dto.PatrimonioID,
                LocalID = dto.LocalID
            };

            _repository.Adicionar(solicitacao);
        }

        public void Responder(Guid transferenciaId, Guid usuarioId, ResponderSolicitacaoTransferenciaDto dto)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitação de transferência não encontrada.");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(solicitacao.PatrimonioID);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
            {
                throw new DomainException("Status pendente não encontrado.");
            }

            if (solicitacao.StatusTransferenciaID != statusPendente.StatusTransferenciaID)
            {
                throw new DomainException("A solicitação de transferência já foi respondida.");
            }

            if (usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDoLocal(usuario.UsuarioID, patrimonio.LocalID);
                if (!usuarioResponsavel)
                {
                    throw new DomainException("O responsável só pode responder solicitações de transferência de patrimônio do ambiente ao qual está vinculado.");
                }
            }

            StatusTransferencia statusResposta;

            if(dto.Aprovado)
            {
                statusResposta = _repository.BuscarStatusTransferenciaPorNome("Aprovado");
            }
            else
            {
                statusResposta = _repository.BuscarStatusTransferenciaPorNome("Recusado");
            }

            if(statusResposta == null)
            {
                throw new DomainException("Status de resposta não encontrado.");
            }

            solicitacao.StatusTransferenciaID = statusResposta.StatusTransferenciaID;
            solicitacao.UsuarioIDAprovacao = usuarioId;
            solicitacao.DataResposta = DateTime.Now;

            _repository.Atualizar(solicitacao);

            if(dto.Aprovado)
            {
                StatusPatrimonio statusTransferido = _repository.BuscarStatusPatrimonioPorNome("Transferido");

                if(statusTransferido == null)
                {
                    throw new  DomainException("Status de patrimonio 'Transferido' não encontrado");
                }

                TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Transferência");

                if (tipoAlteracao == null)
                {
                    throw new DomainException("Tipo Alteração 'Transferência' não encontrado");
                }

                patrimonio.LocalID = solicitacao.LocalID;
                patrimonio.StatusPatrimonioID = statusTransferido.StatusPatrimonioID;

                _repository.AtualizarPatrimonio(patrimonio);

                Log_Patrimonio log = new Log_Patrimonio
                {
                    DataTransferencia = DateTime.Now,
                    TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                    StatusPatrimonioID = statusTransferido.StatusPatrimonioID,
                    PatrimonioID = patrimonio.PatrimonioID,
                    UsuarioID = usuarioId,
                    LocalID = solicitacao.LocalID,
                };

                _repository.AdicionarLog(log);
            }
        }
    }
}
