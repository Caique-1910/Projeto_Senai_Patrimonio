using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface ILocalRepository
    {
        List<Local> Listar();
        Local BuscarPorId(Guid localId);
        void Adicionar(Local local);
        Local BuscarPorNome(string nomeLocal, Guid areaId);
        void Atualizar(Local local);
        bool AreaExiste(Guid areaid);
    }
}
