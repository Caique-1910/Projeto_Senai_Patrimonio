using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.LocalDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class LocalService
    {
        private readonly ILocalRepository _repository;

        public LocalService(ILocalRepository repository) 
        {
            _repository = repository;
        }

        public List<ListarLocalDto> Listar()
        {
            List<Local> locais = _repository.Listar();

            List<ListarLocalDto> locaisDto = locais.Select(local => new ListarLocalDto
            {
                localID = local.LocalID,
                nomeLocal = local.Nome,
                localSap = local.LocalSap,
                descricaoSap = local.Descricao,
                areaID = local.AreaID
            }).ToList();
            
            return locaisDto;
        }

        public ListarLocalDto BuscarPorId(Guid localId) 
        {
            Local local = _repository.BuscarPorId(localId);

            if (local == null) 
            {
                throw new DomainException("Local não encontrado");
            }

            return new ListarLocalDto
            {
                localID = local.LocalID,
                nomeLocal = local.Nome,
                localSap = local.LocalSap,
                descricaoSap = local.Descricao,
                areaID= local.AreaID
            };
        }

        public void Adicionar(CriarLocalDto dto) 
        {
            Validar.ValidarNome(dto.NomeLocal);

            if (!_repository.AreaExiste(dto.areaID))
            {
                throw new DomainException("Área informada não existe");
            }

            Local local = new Local
            {
                Nome = dto.NomeLocal,
                LocalSap = dto.localSap,
                Descricao = dto.descricaoSap,
                AreaID = dto.areaID
            };

            _repository.Adicionar(local);
        }

        public void Atualizar(Guid localId, CriarLocalDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            Local localBanco = _repository.BuscarPorId(localId);

            if (localBanco == null)
            {
                throw new DomainException("Local não encontrado");
            }

            if (!_repository.AreaExiste(dto.areaID))
            {
                throw new DomainException("Área informada não existe.");
            }

            localBanco.Nome = dto.NomeLocal;
            localBanco.LocalSap = dto.localSap;
            localBanco.Descricao = dto.descricaoSap;
            localBanco.AreaID = dto.areaID;


            _repository.Atualizar(localBanco);
        }
    }
}
