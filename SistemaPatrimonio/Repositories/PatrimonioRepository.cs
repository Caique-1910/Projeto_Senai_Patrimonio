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

        public void Atualizar(Patrimonio patrimonio) 
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

            patrimonioBanco.Denominacao = patrimonio.Denominacao;
            patrimonioBanco.Valor = patrimonio.Valor;
            patrimonioBanco.NumeroPatrimonio = patrimonio.NumeroPatrimonio;
            patrimonioBanco.Imagem = patrimonio.Imagem;
            patrimonioBanco.TipoPatrimonioID = patrimonio.TipoPatrimonioID;
            patrimonioBanco.LocalID = patrimonio.LocalID;
            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();
        }

        public Patrimonio BuscarPorNome(string nomePatrimonio)
        {
            return _context.Patrimonio.FirstOrDefault(p => p.Denominacao.ToLower() == nomePatrimonio.ToLower());
        }

        public void AtualizarStatus(Patrimonio patri)
        {
            Patrimonio patrimonio = _context.Patrimonio.Find(patri.StatusPatrimonioID);

            if (patrimonio == null)
            {
                return;
            }

            patrimonio.StatusPatrimonioID = patri.StatusPatrimonioID;
            _context.SaveChanges();
        }
    }
}
