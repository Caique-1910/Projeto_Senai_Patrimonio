using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly SistemaPatrimonioContext _context;

        public EnderecoRepository(SistemaPatrimonioContext context)
        {
            _context = context;
        }

        public List<Endereco> Listar()
        {
            return _context.Endereco.OrderBy(e => e.Logradouro).ToList();
        }

        public Endereco BuscarPorId(Guid enderecoId)
        {
            return _context.Endereco.Find(enderecoId);
        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
        }

        public void Atualizar(Endereco endereco)
        {
            if (endereco == null)
            {
                return;
            }

            Endereco enderecoBanco = _context.Endereco.Find(endereco.EnderecoID);

            if (enderecoBanco == null)
            {
                return;
            }

            enderecoBanco.Logradouro = endereco.Logradouro;
            enderecoBanco.Numero = endereco.Numero;
            enderecoBanco.Complemento = endereco.Complemento;
            enderecoBanco.BairroID = endereco.BairroID;

            _context.SaveChanges();
        }

        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId)
        {
            return _context.Endereco.FirstOrDefault(e => e.Logradouro.ToLower() == logradouro.ToLower() && e.Numero == numero && e.BairroID == bairroId);
        }

        public bool BairroExiste(Guid bairroId)
        {
            return _context.Bairro.Any(b => b.BairroID == bairroId);
        }
    }
}
