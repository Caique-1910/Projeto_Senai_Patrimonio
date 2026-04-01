using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.UsuarioDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<ListarUsuarioDto> listarUsuarioDtos = usuarios.Select(u => new ListarUsuarioDto
            {
                UsuarioID = u.UsuarioID,
                NIF = u.NIF,
                Nome = u.Nome,
                RG = u.RG,
                CPF = u.CPF,
                CarteiraTrabalho = u.CarteiraTrabalho,
                Senha = u.Senha,
                Email = u.Email,
                Ativo = u.Ativo,
                PrimeiroAcesso = u.PrimeiroAcesso
            }).ToList();

            return listarUsuarioDtos;
        }

        public ListarUsuarioDto BuscarPorId(Guid usuarioId)
        {
            Usuario usuario = _repository.BuscarPorId(usuarioId);
            if (usuario == null)
            {
                return null;
            }
            ListarUsuarioDto listarUsuarioDto = new ListarUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                NIF = usuario.NIF,
                Nome = usuario.Nome,
                RG = usuario.RG,
                CPF = usuario.CPF,
                CarteiraTrabalho = usuario.CarteiraTrabalho,
                Senha = usuario.Senha,
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                PrimeiroAcesso = usuario.PrimeiroAcesso
            };
            return listarUsuarioDto;
        }

        public void Adicionar(CriarUsuarioDto dto)
        {
            Validar.ValidarEstado(dto.Nome);

            Usuario usuarioExistente = _repository.BuscarPorNome(dto.Nome);

            if (usuarioExistente != null)
            {
                throw new Exception("Já existe um usuário com esse nome.");
            }

            Usuario usuario = new Usuario
            {
                NIF = dto.NIF,
                Nome = dto.Nome,
                RG = dto.RG,
                CPF = dto.CPF,
                CarteiraTrabalho = dto.CarteiraTrabalho,
                Senha = dto.Senha,
                Email = dto.Email
            };
            _repository.Adicionar(usuario);
        }

        public void Atualizar(Guid id, CriarUsuarioDto dto)
        {
            Validar.ValidarNome(dto.Nome);

            Usuario usuarioExistente = _repository.BuscarPorId(id);

            if (usuarioExistente == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            Usuario usuarioComMesmoNome = _repository.BuscarPorNome(dto.Nome);

            if (usuarioComMesmoNome != null)
            {
                throw new DomainException("Já existe um usuário com esse nome.");
            }

            Usuario usuario = new Usuario
            {
                UsuarioID = id,
                NIF = dto.NIF,
                Nome = dto.Nome,
                RG = dto.RG,
                CPF = dto.CPF,
                CarteiraTrabalho = dto.CarteiraTrabalho,
                Senha = dto.Senha,
                Email = dto.Email
            };
            _repository.Atualizar(usuario);
        }

        public void AtualizarSenha(Guid id, AtualizarSenhaUsuarioDto dto)
        {
            Usuario usuarioExistente = _repository.BuscarPorId(id);

            if (usuarioExistente == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            dto.SenhaAtual = dto.SenhaAtual.Trim();
            dto.NovaSenha = dto.NovaSenha.Trim();

            _repository.AtualizarSenha(usuarioExistente);
        }

        public void AtualizarStatus(Guid id, bool ativo)
        {
            Usuario usuarioExistente = _repository.BuscarPorId(id);

            if (usuarioExistente == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            usuarioExistente.Ativo = ativo;
            _repository.AtualizarStatus(usuarioExistente);
        }

    }
}
