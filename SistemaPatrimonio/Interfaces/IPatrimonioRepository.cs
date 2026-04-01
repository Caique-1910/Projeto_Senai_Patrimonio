using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();
        Patrimonio BuscarPorId(Guid patrimonioId);
        void Adicionar(Patrimonio patrimonio);
        void Atualizar(Patrimonio patrimonio);
        Patrimonio BuscarPorNome(string nomePatrimonio);
        void AtualizarStatus(Patrimonio patrimonio);
    }
}
