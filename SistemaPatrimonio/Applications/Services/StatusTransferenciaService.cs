using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.StatusTransferenciaDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class StatusTransferenciaService
    {
        private readonly IStatusTransferenciaRepository _repository;

        public StatusTransferenciaService(IStatusTransferenciaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusTransferenciaDto> Listar()
        {
            List<StatusTransferencia> statusTransferencias = _repository.Listar();

            List<ListarStatusTransferenciaDto> statusTransferenciaDtos = statusTransferencias.Select(st => new ListarStatusTransferenciaDto
            {
                StatusTransferenciaID = st.StatusTransferenciaID,
                Status = st.Status
            }).ToList();

            return statusTransferenciaDtos;
        }

        public ListarStatusTransferenciaDto BuscarPorId(Guid id)
        {
            StatusTransferencia st = _repository.BuscarPorId(id);
            
            if(st == null)
            {
                throw new DomainException("Status transferência não encontrado");
            }

            return new ListarStatusTransferenciaDto
            {
                StatusTransferenciaID = st.StatusTransferenciaID,
                Status = st.Status
            };
        }

        public void Adicionar(CriarStatusTransferenciaDto dto)
        {
            Validar.ValidarNome(dto.Status);

            StatusTransferencia stExistente = _repository.BuscarPorNome(dto.Status);

            if (stExistente != null)
            {
                throw new Exception("Já existe um tipo de alteração com esse nome.");
            }

            StatusTransferencia st = new StatusTransferencia
            {
                Status = dto.Status
            };

            _repository.Adicionar(st);
        }

        public void Atualizar(Guid id, CriarStatusTransferenciaDto dto)
        {
            Validar.ValidarNome(dto.Status);

            StatusTransferencia stbanco = _repository.BuscarPorId(id);

            if(stbanco == null)
            {
                throw new DomainException("Status de transferência não encontrado");
            }

            StatusTransferencia stComMesmoNome = _repository.BuscarPorNome(dto.Status);

            if (stComMesmoNome != null)
            {
                throw new Exception("Já existe um status transferência com esse nome.");
            }

            stbanco.Status = dto.Status;

           _repository.Atualizar(stbanco);
        }
    }
}
