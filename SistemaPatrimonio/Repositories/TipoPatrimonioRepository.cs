using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class TipoPatrimonioRepository : ITipoPatrimonioRepository
    {
        private readonly SistemaPatrimonioContext _context;

        public TipoPatrimonioRepository(SistemaPatrimonioContext context)
        {
            _context = context;
        }

        public List<TipoPatrimonio> Listar()
        {
            return _context.TipoPatrimonio.OrderBy(tp => tp.NomeTipo).ToList();
        }

        public TipoPatrimonio BuscarPorId(Guid tipoPatrimonioId)
        {
            return _context.TipoPatrimonio.FirstOrDefault(tp => tp.TipoPatrimonioID == tipoPatrimonioId);
        }

        public void Adicionar(TipoPatrimonio tipoPatrimonio)
        {
            _context.TipoPatrimonio.Add(tipoPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(TipoPatrimonio tipoPatrimonio)
        {
            if (tipoPatrimonio == null)
            {
                return;
            }

            TipoPatrimonio tpBanco = _context.TipoPatrimonio.Find(tipoPatrimonio.TipoPatrimonioID);

            if (tpBanco == null)
            {
                return;
            }

            tpBanco.NomeTipo = tipoPatrimonio.NomeTipo;
            _context.SaveChanges();
        }

        public TipoPatrimonio BuscarPorNome(string nomeTipo)
        {
            return _context.TipoPatrimonio.FirstOrDefault(tp => tp.NomeTipo.ToLower() == nomeTipo.ToLower());
        }
    }
}
