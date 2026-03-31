using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Repositories
{
    public class StatusTransferenciaRepository : IStatusTransferenciaRepository
    {
        private readonly SistemaPatrimonioContext _context;

        public StatusTransferenciaRepository(SistemaPatrimonioContext context)
        {
            _context = context;
        }

        public List<StatusTransferencia> Listar()
        {
          return _context.StatusTransferencia.OrderBy(st => st.Status).ToList();
        }

        public StatusTransferencia BuscarPorId(Guid statusTransferenciaId)
        {
            return _context.StatusTransferencia.FirstOrDefault(st => st.StatusTransferenciaID == statusTransferenciaId);
        }

        public void Adicionar(StatusTransferencia statusTransferencia)
        {
            _context.StatusTransferencia.Add(statusTransferencia);
            _context.SaveChanges();
        }

        public void Atualizar(StatusTransferencia statusTransferencia)
        {
            if (statusTransferencia == null)
            {
                return;
            }

            StatusTransferencia stBanco = _context.StatusTransferencia.Find(statusTransferencia.StatusTransferenciaID);

            if (stBanco == null)
            {
                return;
            }

            stBanco.Status = statusTransferencia.Status;
            _context.SaveChanges();
        }

        public StatusTransferencia BuscarPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(st => st.Status.ToLower() == nomeStatus.ToLower());
        }
    }
}
