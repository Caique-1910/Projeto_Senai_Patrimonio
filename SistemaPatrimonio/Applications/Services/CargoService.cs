using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.CargoDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class CargoService
    {
        private readonly ICargoRepository _repository;

        public CargoService(ICargoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCargoDto> Listar()
        {
            List<Cargo> cargos = _repository.Listar();

            List<ListarCargoDto> cargoDtos = cargos.Select(c => new ListarCargoDto
            {
                CargoId = c.CargoID,
                NomeCargo = c.NomeCargo
            }).ToList();

            return cargoDtos;
        }

        public ListarCargoDto BuscarPorId(Guid cargoId)
        {
            Cargo cargo = _repository.BuscarPorId(cargoId);

            if (cargo == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }

            return new ListarCargoDto
            {
                CargoId = cargo.CargoID,
                NomeCargo = cargo.NomeCargo
            };
        }

        public void Adicionar(CriarCargoDto dto)
        {
            Validar.ValidarNome(dto.NomeCargo);

            Cargo cargoExistetente = _repository.BuscarPorNome(dto.NomeCargo);

            if (cargoExistetente != null)
            {
                throw new DomainException("Já existe um cargo cadastrada com esse nome.");
            }

            Cargo cargo = new Cargo
            {
                NomeCargo = dto.NomeCargo
            };

            _repository.Adicionar(cargo);
        }

        public void Atualizar(Guid id, CriarCargoDto dto)
        {
            Validar.ValidarNome(dto.NomeCargo);

            Cargo cargoExistente = _repository.BuscarPorId(id);

            if (cargoExistente == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }

            Cargo cargoComMesmoNome = _repository.BuscarPorNome(dto.NomeCargo);

            if (cargoComMesmoNome != null && cargoComMesmoNome.CargoID != id)
            {
                throw new DomainException("Já existe um cargo cadastrada com esse nome.");
            }

            cargoExistente.NomeCargo = dto.NomeCargo;
            _repository.Atualizar(cargoExistente);
        }
    }
}
