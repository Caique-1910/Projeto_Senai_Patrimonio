using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.TipoPatrimonioDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class TipoPatrimonioService
    {
        private readonly ITipoPatrimonioRepository _repository;

        public TipoPatrimonioService(ITipoPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoPatrimonioDto> Listar()
        {
            List<TipoPatrimonio> tipoPatrimonios = _repository.Listar();

            List<ListarTipoPatrimonioDto> tipoPatrimonioDtos = tipoPatrimonios.Select(tp => new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tp.TipoPatrimonioID,
                NomeTipo = tp.NomeTipo
            }).ToList();

            return tipoPatrimonioDtos;
        }

        public ListarTipoPatrimonioDto BuscarPorId(Guid tipoPatrimonioId)
        {
            TipoPatrimonio tipoPatrimonio = _repository.BuscarPorId(tipoPatrimonioId);

            if (tipoPatrimonio == null)
            {
                throw new DomainException("Tipo patrimonio não encontrado");
            }

            return new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tipoPatrimonio.TipoPatrimonioID,
                NomeTipo = tipoPatrimonio.NomeTipo
            };
        }

        public void Adicionar(CriarTipoPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio tipoPatrimonioExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoPatrimonioExistente != null)
            {
                throw new Exception("Já existe um tipo de patrimônio com esse nome.");
            }

            TipoPatrimonio tipoPatrimonio = new TipoPatrimonio
            {
                TipoPatrimonioID = Guid.NewGuid(),
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipoPatrimonio);
        }

        public void Atualizar(Guid id, CriarTipoPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio tipoPatrimonioExistente = _repository.BuscarPorId(id);

            if (tipoPatrimonioExistente == null)
            {
                throw new Exception("Tipo de patrimônio não encontrado.");
            }

            TipoPatrimonio tipoPatrimonioComMesmoNome = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoPatrimonioComMesmoNome != null)
            {
                throw new Exception("Já existe um tipo de patrimônio com esse nome.");
            }

            tipoPatrimonioExistente.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tipoPatrimonioExistente);
        }
    }
}
