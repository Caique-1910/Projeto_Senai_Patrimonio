using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class SolicitacaoTransferenciaRepository : ISolicitacaoTransferenciaRepository
    {
        private readonly SistemaPatrimonioContext _context;

        public SolicitacaoTransferenciaRepository(SistemaPatrimonioContext context)
        {
            _context = context;
        }


        public List<SolicitacaoTransferencia> Listar()
        {
            return _context.SolicitacaoTransferencia.ToList();
        }

        public SolicitacaoTransferencia ObterPorId(Guid id)
        {
            return _context.SolicitacaoTransferencia.FirstOrDefault(s => s.TransferenciaID == id);
        }

        public void Adicionar(SolicitacaoTransferencia solicitacao)
        {
            _context.SolicitacaoTransferencia.Add(solicitacao);
            _context.SaveChanges();
        }

        public void Atualizar(SolicitacaoTransferencia solicitacao)
        {
            if (solicitacao == null)
            {
                return;
            }

            SolicitacaoTransferencia solicitacaoBanco = _context.SolicitacaoTransferencia.Find(solicitacao.TransferenciaID);

            if (solicitacaoBanco == null)
            {
                return;
            }

            solicitacaoBanco.DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitante;
            solicitacaoBanco.DataResposta = solicitacao.DataResposta;
            solicitacaoBanco.Justificativa = solicitacao.Justificativa;
            solicitacaoBanco.PatrimonioID = solicitacao.PatrimonioID;
            solicitacaoBanco.StatusTransferenciaID = solicitacao.StatusTransferenciaID;
            solicitacaoBanco.StatusPatrimonioID = solicitacao.StatusPatrimonioID;
            solicitacaoBanco.UsuarioIDSolicitacao = solicitacao.UsuarioIDSolicitacao;
            solicitacaoBanco.UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao;
            solicitacaoBanco.LocalID = solicitacao.LocalID;

            _context.SaveChanges();
        }

        public SolicitacaoTransferencia ObrterPorJustificativa(string jus)
        {
            return _context.SolicitacaoTransferencia.FirstOrDefault(p => p.Justificativa.ToLower() == jus.ToLower());
        }
    }
}
