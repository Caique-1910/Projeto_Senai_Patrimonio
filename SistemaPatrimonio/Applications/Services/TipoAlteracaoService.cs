using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.TipoAlteracaoDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class TipoAlteracaoService
    {
        private readonly ITipoAlteracaoRepository _repository;

        public TipoAlteracaoService(ITipoAlteracaoRepository repository)
        {
          _repository = repository;
        }

        public List<ListarTipoAlteracaoDto> Listar() 
        {
            List<TipoAlteracao> tiposAlteracoes = _repository.Listar();

            List<ListarTipoAlteracaoDto> tipoAlteracoesDtos = tiposAlteracoes.Select(ta => new ListarTipoAlteracaoDto
            {
                TipoAlteracaoID = ta.TipoAlteracaoID,
                Tipo = ta.Tipo
            }).ToList();

            return tipoAlteracoesDtos;
        }


        public ListarTipoAlteracaoDto BuscarPorId(Guid tipoAlteracaoId) 
        {
            TipoAlteracao tipoAlteracao = _repository.BuscarPorId(tipoAlteracaoId);

            if (tipoAlteracao == null) 
            {
                throw new DomainException("Tipo alteração não encontrado");
            }

            return new ListarTipoAlteracaoDto
            {
                TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                Tipo = tipoAlteracao.Tipo
            };
        }

        public void Adicionar(CriarTipoAlteracaoDto dto) 
        {
            Validar.ValidarNome(dto.TipoAlteracao);

            TipoAlteracao tipoAlteracaoExistente = _repository.BuscarPorNome(dto.TipoAlteracao);

            if (tipoAlteracaoExistente != null) 
            {
                throw new DomainException("Já existe um tipo de alteração com esse nome.");
            }

            TipoAlteracao tipoAlteracao = new TipoAlteracao
            {
                Tipo = dto.TipoAlteracao
            };

            _repository.Adicionar(tipoAlteracao);
        }

        public void Atualizar(Guid id, CriarTipoAlteracaoDto dto)
        {
            Validar.ValidarNome(dto.TipoAlteracao);

            TipoAlteracao tipoAlteracaoExistente = _repository.BuscarPorId(id);

            if (tipoAlteracaoExistente == null)
            {
                throw new DomainException("Tipo de alteração não encontrado.");
            }

            TipoAlteracao tipoAlteracaoComMesmoNome = _repository.BuscarPorNome(dto.TipoAlteracao);

            if (tipoAlteracaoComMesmoNome != null)
            {
                throw new DomainException("Já existe um tipo de alteração com esse nome.");
            }


            tipoAlteracaoExistente.Tipo = dto.TipoAlteracao;

            _repository.Atualizar(tipoAlteracaoExistente);
        }
    }
}
