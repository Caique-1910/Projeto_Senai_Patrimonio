using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class LocalRepository : ILocalRepository
    {
        private readonly SistemaPatrimonioContext _context;

        public LocalRepository(SistemaPatrimonioContext context)
        {
            _context = context;
        }

        public List<Local> Listar()
        {
            return _context.Local.OrderBy(local => local.Nome).ToList();
        }

        public Local BuscarPorId(Guid localId)
        {
            return _context.Local.Find(localId);
        }

        public void Adicionar(Local local) 
        { 
            _context.Local.Add(local);
            _context.SaveChanges();
        }

        public void Atualizar(Local local)
        {
            if (local == null)
            {
                return;
            }

            Local localBanco = _context.Local.Find(local.LocalID);

            if (localBanco == null) 
            {
                return ;
            }

            localBanco.Nome = local.Nome;
            localBanco.LocalSap = local.LocalSap;
            localBanco.Descricao = local.Descricao;
            localBanco.AreaID = local.AreaID;
            
            _context.SaveChanges();
        }

        public bool AreaExiste(Guid areaId)
        {
            return _context.Area.Any(area => area.AreaID == areaId);
        }
    }
}
