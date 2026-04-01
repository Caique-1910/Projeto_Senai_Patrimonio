using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly SistemaPatrimonioContext _context;

        public CargoRepository(SistemaPatrimonioContext context)
        {
          _context = context;
        }
    
        public List<Cargo> Listar()
        {
            return _context.Cargo.OrderBy(c => c.NomeCargo). ToList();
        }

        public Cargo BuscarPorId(Guid cargoId)
        {
            return _context.Cargo.Find(cargoId);
        }

        public void Adicionar(Cargo cargo)
        {
            _context.Cargo.Add(cargo);
            _context.SaveChanges();
        }

        public void Atualizar(Cargo cargo)
        {
            if (cargo == null)
            {
                return;
            }

            Cargo cargoBanco = _context.Cargo.Find(cargo.CargoID);

            if (cargoBanco == null)
            {
                return;
            }

            cargoBanco.NomeCargo = cargo.NomeCargo;
            _context.SaveChanges();
        }

        public Cargo BuscarPorNome(string nomeCargo)
        {
            return _context.Cargo.FirstOrDefault(c => c.NomeCargo.ToLower() == nomeCargo.ToLower());
        }
    }
}
