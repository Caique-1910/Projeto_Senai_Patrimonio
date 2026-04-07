using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.LogPatrimonioDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class LogPatrimonioService
    {
        private readonly ILogPatrimonioRepository _repository;

        public LogPatrimonioService(ILogPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLogPatrimonioDto> Listar()
        {
            List<Log_Patrimonio> logs = _repository.Listar();

            List<ListarLogPatrimonioDto> logsDto = logs.Select(log => new ListarLogPatrimonioDto
            {
                LogPatrimonioID = log.LogPatrimonioID,
                DataTransferencia = log.DataTransferencia,
                PatrimonioID = log.PatrimonioID,
                DenomoinacaoPatrimonio = log.Patrimonio.Denominacao,
                TipoAlteracao = log.TipoAlteracao.Tipo,
                StautusPatrimonio = log.StatusPatrimonio.Status,
                Usuario = log.Usuario.Nome,
                Local = log.Local.Nome
            }).ToList();

            return logsDto;
        }

        public List<ListarLogPatrimonioDto> BuscarPorPatrimonio(Guid patrimonioId) 
        {
            List<Log_Patrimonio> logs = _repository.BuscarPorPatrimonio(patrimonioId);

            if(logs == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            List<ListarLogPatrimonioDto> logsDto = logs.Select(log => new ListarLogPatrimonioDto
            {
                LogPatrimonioID = log.LogPatrimonioID,
                DataTransferencia = log.DataTransferencia,
                PatrimonioID = log.PatrimonioID,
                DenomoinacaoPatrimonio = log.Patrimonio.Denominacao,
                TipoAlteracao = log.TipoAlteracao.Tipo,
                StautusPatrimonio = log.StatusPatrimonio.Status,
                Usuario = log.Usuario.Nome,
                Local = log.Local.Nome
            }).ToList();

            return logsDto;
        }
    }
}
