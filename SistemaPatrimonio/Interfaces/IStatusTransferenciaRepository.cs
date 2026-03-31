using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface IStatusTransferenciaRepository
    {
        List<StatusTransferencia> Listar();
        StatusTransferencia BuscarPorId(Guid statusTransferenciaId);
        void Adicionar(StatusTransferencia statusTransferencia);
        void Atualizar(StatusTransferencia statusTransferencia);
        StatusTransferencia BuscarPorNome(string nomeStatus);
    }
}
