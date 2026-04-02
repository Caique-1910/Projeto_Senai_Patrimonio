using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        List<SolicitacaoTransferencia> Listar();
        SolicitacaoTransferencia ObterPorId(Guid id);
        void Adicionar(SolicitacaoTransferencia solicitacao);
        void Atualizar(SolicitacaoTransferencia solicitacao);
        public SolicitacaoTransferencia ObterPorJustificativa(string jus);
    }
}
