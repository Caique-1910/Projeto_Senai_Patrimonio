using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.StatusPatrimonioDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonioRepository _repository;

        public StatusPatrimonioService(IStatusPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusPatrimonioDto> Listar()
        {
            List<StatusPatrimonio> statusPatrimonios = _repository.Listar();

            List<ListarStatusPatrimonioDto> statusPatrimonioDtos = statusPatrimonios.Select(sp => new ListarStatusPatrimonioDto
            {
                StatusPatrimonioId = sp.StatusPatrimonioID,
                NomeStatus = sp.Status
            }).ToList();

            return statusPatrimonioDtos;
        }

        public ListarStatusPatrimonioDto BuscarPorId(Guid id)
        {
            StatusPatrimonio sp = _repository.BuscarPorId(id);

            if (sp == null)
            {
                throw new DomainException("Status patrimônio não encontrado");
            }

            return new ListarStatusPatrimonioDto
            {
                StatusPatrimonioId = sp.StatusPatrimonioID,
                NomeStatus = sp.Status
            };
        }

        public void Adicionar(CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio spExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (spExistente != null)
            {
                throw new DomainException("Já existe um status de patrimônio com esse nome.");
            }

            StatusPatrimonio sp = new StatusPatrimonio
            {
                StatusPatrimonioID = Guid.NewGuid(),
                Status = dto.NomeStatus
            };

            _repository.Adicionar(sp);
        }

        public void Atualizar(Guid id, CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio spExistente = _repository.BuscarPorId(id);

            if (spExistente == null)
            {
                throw new DomainException("Status patrimônio não encontrado");
            }

            StatusPatrimonio spComMesmoNome = _repository.BuscarPorNome(dto.NomeStatus);

            if (spComMesmoNome != null)
            {
                throw new DomainException("Já existe um status de patrimônio com esse nome.");
            }

            spExistente.Status = dto.NomeStatus;
            _repository.Atualizar(spExistente);
        }
    }
}
