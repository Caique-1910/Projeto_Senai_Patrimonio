using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly SistemaPatrimonioContext _context;
        
        public CidadeRepository(SistemaPatrimonioContext context)
        {
            _context = context;
        }

        public List<Cidade> Listar()
        {
            return _context.Cidade.OrderBy(cidade => cidade.NomeCidade).ToList();
        }

        public Cidade BuscarPorId(Guid cidadeId)
        {
            return _context.Cidade.Find(cidadeId);
        }

        public Cidade BuscarPorNomeEEstado(string nomeCidade, string nomeEstado)
        {
            return _context.Cidade.FirstOrDefault(cidade => cidade.NomeCidade.ToLower() == nomeCidade.ToLower() && cidade.Estado == nomeEstado);
        }

        public void Adicionar(Cidade cidade)
        {
            _context.Cidade.Add(cidade);
            _context.SaveChanges();
        }

        public void Atualizar(Cidade cidade)
        {
            if (cidade == null)
            {
                return;
            }

            Cidade cidadeBanco = _context.Cidade.Find(cidade.CidadeID);

            cidadeBanco.NomeCidade = cidade.NomeCidade;
            cidadeBanco.Estado = cidade.Estado;
           

            _context.SaveChanges();
        }
    }
}
