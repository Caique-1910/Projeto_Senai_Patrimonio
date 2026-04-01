using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface ICargoRepository
    {
        List<Cargo> Listar();
        Cargo BuscarPorId(Guid cargoId);
        void Adicionar(Cargo cargo);
        void Atualizar(Cargo cargo);
        Cargo BuscarPorNome(string nomeCargo);
    }
}
