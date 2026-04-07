using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface ILogPatrimonioRepository
    {
        List<Log_Patrimonio> Listar();
        List<Log_Patrimonio> BuscarPorPatrimonio(Guid patrimonioId);
    }
}
