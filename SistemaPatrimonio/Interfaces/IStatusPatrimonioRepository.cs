using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface IStatusPatrimonioRepository
    {
            List<StatusPatrimonio> Listar();
            StatusPatrimonio BuscarPorId(Guid statusPatrimonioId);
            void Adicionar(StatusPatrimonio statusPatrimonio);
            void Atualizar(StatusPatrimonio statusPatrimonio);
            StatusPatrimonio BuscarPorNome(string nomeStatus);
    }
}
