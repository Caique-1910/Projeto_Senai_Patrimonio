using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly SistemaPatrimonioContext _context;

        public TipoUsuarioRepository(SistemaPatrimonioContext context)
        {
            _context = context;
        }

        public List<TipoUsuario> Listar()
        {
            return _context.TipoUsuario.OrderBy(tu => tu.NomeTipo).ToList();
        }

        public TipoUsuario BuscarPorId(Guid tipoUsuarioId)
        {
            return _context.TipoUsuario.FirstOrDefault(tu => tu.TipoUsuarioID == tipoUsuarioId);
        }

        public void Adicionar(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuario.Add(tipoUsuario);
            _context.SaveChanges();
        }

        public void Atualizar(TipoUsuario tipoUsuario)
        {
            if (tipoUsuario == null)
            {
                return;
            }

            TipoUsuario tpbanco = _context.TipoUsuario.Find(tipoUsuario.TipoUsuarioID);

            if (tpbanco == null)
            {
                return;
            }

            tpbanco.NomeTipo = tipoUsuario.NomeTipo;

            _context.SaveChanges();
        }

        public TipoUsuario BuscarPorNome(string nomeTipo)
        {
            return _context.TipoUsuario.FirstOrDefault(tu => tu.NomeTipo.ToLower() == nomeTipo.ToLower());
        }
    }
}
