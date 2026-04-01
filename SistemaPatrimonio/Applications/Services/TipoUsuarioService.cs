using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.TipoUsuarioDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class TipoUsuarioService
    {
        private readonly ITipoUsuarioRepository _repository;

        public TipoUsuarioService(ITipoUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoUsuarioDto> Listar()
        {
            List<TipoUsuario> tipoUsuarios = _repository.Listar();

            List<ListarTipoUsuarioDto> tipoUsuarioDtos = tipoUsuarios.Select(tu => new ListarTipoUsuarioDto
            {
                TipoUsuarioID = tu.TipoUsuarioID,
                NomeTipo = tu.NomeTipo
            }).ToList();

            return tipoUsuarioDtos;
        }

        public ListarTipoUsuarioDto BuscarPorId(Guid tipoUsuarioId)
        {
            TipoUsuario tipoUsuario = _repository.BuscarPorId(tipoUsuarioId);

            if (tipoUsuario == null)
            {
                throw new Exception("Tipo de usuário não encontrado");
            }

            return new ListarTipoUsuarioDto
            {
                TipoUsuarioID = tipoUsuario.TipoUsuarioID,
                NomeTipo = tipoUsuario.NomeTipo
            };
        }

        public void Adicionar(CriarTipoUsuarioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoUsuario tipoUsuarioExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoUsuarioExistente != null)
            {
                throw new DomainException("Já existe um tipo de usuário com esse nome.");
            }

            TipoUsuario tipoUsuario = new TipoUsuario
            {
                TipoUsuarioID = Guid.NewGuid(),
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipoUsuario);
        }

        public void Atualizar(Guid id, CriarTipoUsuarioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoUsuario tipoUsuarioExistente = _repository.BuscarPorId(id);

            if (tipoUsuarioExistente == null)
            {
                throw new DomainException("Tipo de usuário não encontrado.");
            }

            TipoUsuario tipoUsuarioComMesmoNome = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoUsuarioComMesmoNome != null )
            {
                throw new DomainException("Já existe um tipo de usuário com esse nome.");
            }

            tipoUsuarioExistente.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tipoUsuarioExistente);
        }
    }
}
