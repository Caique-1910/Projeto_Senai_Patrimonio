using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        List<SolicitacaoTransferencia> Listar();
        SolicitacaoTransferencia BuscarPorId(Guid id);
        bool ExisteSolicitacaoPendente(Guid patrimonioId);
        bool UsuarioResponsavelDoLocal(Guid usuarioId, Guid localId);
        StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus);
        void Adicionar(SolicitacaoTransferencia solicitacao);
        bool LocalExiste(Guid localId);
        Patrimonio BuscarPatrimonioPorId(Guid patrimonioId);
    }
}
