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
            return _context.SolicitacaoTransferencia.OrderByDescending(s => s.DataCriacaoSolicitante).ToList();
        }

        public SolicitacaoTransferencia BuscarPorId(Guid id)
        {
            return _context.SolicitacaoTransferencia.Find(id);
        }

        public StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(s => s.Status.ToLower() == nomeStatus.ToLower());
        }

        public bool ExisteSolicitacaoPendente(Guid patrimonioId)
        {
            StatusTransferencia statusPendente = BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if(statusPendente == null)
            {
                return false;
            }

            return _context.SolicitacaoTransferencia.Any(s => s.PatrimonioID == patrimonioId && s.StatusPatrimonioID == statusPendente.StatusTransferenciaID);
        }

        public bool UsuarioResponsavelDoLocal(Guid usuarioId, Guid localId)
        {
            return _context.Usuario.Any(u => u.UsuarioID == usuarioId && u.LocalLocalUsuario.Any(l => l.LocalID == localId));
        }

        public void Adicionar(SolicitacaoTransferencia solicitacao)
        {
            _context.SolicitacaoTransferencia.Add(solicitacao);
            _context.SaveChanges();
        }

        public bool LocalExiste(Guid localId)
        {
            return _context.Local.Any(l => l.LocalID == localId);
        }

        public Patrimonio BuscarPatrimonioPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId);
        }
    }
}
