using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {
        private readonly SistemaPatrimonioContext _context;

        public StatusPatrimonioRepository(SistemaPatrimonioContext context)
        {
            _context = context;
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio.OrderBy(sp => sp.Status).ToList();
        }

        public StatusPatrimonio BuscarPorId(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.Find(statusPatrimonioId);
        }

        public void Adicionar(StatusPatrimonio statusPatrimonio)
        {
            _context.StatusPatrimonio.Add(statusPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(StatusPatrimonio statusPatrimonio)
        {
            if (statusPatrimonio == null)
            {
                return;
            }

            StatusPatrimonio statusPatrimonioBanco = _context.StatusPatrimonio.Find(statusPatrimonio.StatusPatrimonioID);

            if (statusPatrimonioBanco == null)
            {
                return;
            }

            statusPatrimonioBanco.Status = statusPatrimonio.Status;
            _context.SaveChanges();
        }

        public StatusPatrimonio BuscarPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(sp => sp.Status.ToLower() == nomeStatus.ToLower());
        }
    }
}
