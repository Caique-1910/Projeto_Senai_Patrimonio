using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
       private readonly SistemaPatrimonioContext _context;

       public PatrimonioRepository(SistemaPatrimonioContext context)
       {
          _context = context;
       }

        public List<Patrimonio> Listar() 
        { 
            return _context.Patrimonio.OrderBy(p => p.Denominacao).ToList();
        }

        public Patrimonio BuscarPorId(Guid patrimonioId) 
        {
            return _context.Patrimonio.Find(patrimonioId);
        }

        public void Adicionar(Patrimonio patrimonio) 
        {
            _context.Patrimonio.Add(patrimonio);
            _context.SaveChanges();
        }

        public bool BuscarPorNumeroPatrimonio(string numeroPatrimonio)
        {
            return _context.Patrimonio.Any(p => p.NumeroPatrimonio == numeroPatrimonio);
        }

        public bool LocalExiste(Guid localId)
        {
            return _context.Local.Any(l => l.LocalID == localId);
        }

        public bool StatusPatrimonioExiste(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.Any(st => st.StatusPatrimonioID == statusPatrimonioId);
        }

        public Local BuscarLocalPorNome(string nomeLocal)
        {
            return _context.Local.FirstOrDefault(l => l.Nome.ToLower() == nomeLocal.ToLower());
        }

        public StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(s => s.Status.ToLower() == nomeStatus.ToLower());
        }

        public TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(t=> t.Tipo.ToLower() == nomeTipo.ToLower());
        }


        public void AtualizarStatus(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);

            if (patrimonioBanco == null)
            {
                return;
            }

            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();
        }

        public void AdicionarLog(Log_Patrimonio logPatrimonio)
        {
            _context.Log_Patrimonio.Add(logPatrimonio);
            _context.SaveChanges();
        }

    }
}
