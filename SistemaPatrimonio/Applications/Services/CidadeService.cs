using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.CidadeDto;
using SistemaPatrimonio.Repositories;

namespace SistemaPatrimonio.Applications.Services
{
    public class CidadeService
    {
        private readonly CidadeRepository _repository;

        public CidadeService(CidadeRepository repository)
        {
            _repository = repository; 
        }

        public List<ListarCidadeDto> Listar()
        {
            List<Cidade> cidades = _repository.Listar();

            List<ListarCidadeDto> cidadesDto = cidades.Select(cidade => new ListarCidadeDto
            {
                CidadeId = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado
            }).ToList();

            return cidadesDto;
        }
    }
}
