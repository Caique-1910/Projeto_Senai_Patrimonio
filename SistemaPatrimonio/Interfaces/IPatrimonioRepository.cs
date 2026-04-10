using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();
        Patrimonio BuscarPorId(Guid patrimonioId);
        bool BuscarPorNumeroPatrimonio(string numeroPatrimonio);
        void Adicionar(Patrimonio patrimonio);
        void AtualizarStatus(Patrimonio patrimonio);
        void AdicionarLog(Log_Patrimonio log);
        bool LocalExiste(Guid localID);
        bool StatusPatrimonioExiste(Guid statusPatrimonioID);
        Local BuscarLocalPorNome(string nomeLocal);
        StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus);
        TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo);
    }
}
