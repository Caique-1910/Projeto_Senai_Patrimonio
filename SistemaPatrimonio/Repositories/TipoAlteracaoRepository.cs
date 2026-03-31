using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class TipoAlteracaoRepository : ITipoAlteracaoRepository
    {
        private readonly SistemaPatrimonioContext _context;

        public TipoAlteracaoRepository(SistemaPatrimonioContext context)
        {
            _context = context;
        }

        public List<TipoAlteracao> Listar()
        {
            return _context.TipoAlteracao.OrderBy(tpa => tpa.Tipo).ToList();
        }

        public TipoAlteracao BuscarPorId(Guid tipoAlteracaoId)
        {
            return _context.TipoAlteracao.FirstOrDefault(ta => ta.TipoAlteracaoID == tipoAlteracaoId);
        }

        public void Adicionar(TipoAlteracao tipoAlteracao)
        {
            _context.TipoAlteracao.Add(tipoAlteracao);
            _context.SaveChanges();
        }

        public void Atualizar(TipoAlteracao tipoAlteracao)
        {
            if (tipoAlteracao == null)
            {
                return;
            }

            TipoAlteracao tipoAlteracaoBanco = _context.TipoAlteracao.Find(tipoAlteracao.TipoAlteracaoID);

            tipoAlteracaoBanco.Tipo = tipoAlteracao.Tipo;

            _context.SaveChanges();
        }

        public TipoAlteracao BuscarPorNome(string nomeTipoAlteracao)
        {
            return _context.TipoAlteracao.FirstOrDefault(ta => ta.Tipo.ToLower() == nomeTipoAlteracao.ToLower());
        }
    }
}
