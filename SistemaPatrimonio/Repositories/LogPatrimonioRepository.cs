using Microsoft.EntityFrameworkCore;
using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class LogPatrimonioRepository : ILogPatrimonioRepository
    {
        private readonly SistemaPatrimonioContext _context;

        public LogPatrimonioRepository(SistemaPatrimonioContext context)
        {
            _context = context;
        }

        public List<Log_Patrimonio> Listar()
        {
            return _context.Log_Patrimonio.Include(l => l.Usuario).Include(l => l.Local).Include(l => l.TipoAlteracao).Include(l => l.StatusPatrimonio).Include(l => l.Patrimonio).OrderByDescending(l => l.DataTransferencia).ToList();
        }

        public List<Log_Patrimonio> BuscarPorPatrimonio(Guid patrimonioId)
        {
            return _context.Log_Patrimonio.Include(l => l.Usuario).Include(l => l.Local).Include(l => l.TipoAlteracao).Include(l => l.StatusPatrimonio).Include(l => l.Patrimonio).Where(l => l.PatrimonioID == patrimonioId).OrderByDescending(l => l.DataTransferencia).ToList();
        }
    }
}
