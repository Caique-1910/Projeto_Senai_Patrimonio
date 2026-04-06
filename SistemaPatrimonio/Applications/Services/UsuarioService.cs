using SistemaPatrimonio.Applications.Autenticacao;
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
                throw new DomainException("Usuário não encontrado.");
            }

            ListarUsuarioDto usuarioDto = new ListarUsuarioDto
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

            return usuarioDto;
        }

        public void Adicionar(CriarUsuarioDto dto)
        {
            Validar.ValidarNome(dto.Nome);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);
            
            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email);

            if (usuarioDuplicado != null)
            {
                if(usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Já existe um usuário com esse NIF.");
                }

                if(usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Já existe um usuário com esse CPF.");
                }

                if(usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Já existe um usuário com esse email.");
                }
            }

            if (!_repository.EnderecoExiste(dto.EnderecoID))
            {
                throw new DomainException("Endereço não existe.");
            }

            if (!_repository.CargoExiste(dto.CargoID))
            {
                throw new DomainException("Cargo não existe.");
            }

            if (!_repository.TipoUsuarioExiste(dto.TipoUsuarioID))
            {
                throw new DomainException("Tipo usuário não existe.");
            }

            Usuario usuario = new Usuario
            {
                NIF = dto.NIF,
                Nome = dto.Nome,
                RG = dto.RG,
                CPF = dto.CPF,
                CarteiraTrabalho = dto.CarteiraTrabalho,
                Senha = CriptografiaUsuario.CriptografarSenha(dto.NIF),
                Email = dto.Email,
                Ativo = true,
                PrimeiroAcesso = true,
                EnderecoID = dto.EnderecoID,
                CargoID = dto.CargoID,
                TipoUsuarioID = dto.TipoUsuarioID,
            };

            _repository.Adicionar(usuario);
        }

        public void Atualizar(Guid id, CriarUsuarioDto dto)
        {
            Validar.ValidarNome(dto.Nome);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);

            Usuario usuarioBanco = _repository.BuscarPorId(id);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email, id);

            if (usuarioDuplicado != null)
            {
                if(usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Já existe um usuário com esse NIF.");
                }

                if(usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Já existe um usuário com esse CPF.");
                }

                if(usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Já existe um usuário com esse email.");
                }
            }

            if (!_repository.EnderecoExiste(dto.EnderecoID))
            {
                throw new DomainException("Endereço não existe.");
            }

            if (!_repository.CargoExiste(dto.CargoID))
            {
                throw new DomainException("Cargo não existe.");
            }

            if (!_repository.TipoUsuarioExiste(dto.TipoUsuarioID))
            {
                throw new DomainException("Tipo usuário não existe.");
            }

            usuarioBanco.NIF = dto.NIF;
            usuarioBanco.Nome = dto.Nome;
            usuarioBanco.RG = dto.RG;
            usuarioBanco.CPF = dto.CPF;
            usuarioBanco.CarteiraTrabalho = dto.CarteiraTrabalho;
            usuarioBanco.Email = dto.Email;
            usuarioBanco.EnderecoID = dto.EnderecoID;
            usuarioBanco.CargoID = dto.CargoID;
            usuarioBanco.TipoUsuarioID = dto.TipoUsuarioID;

            _repository.Atualizar(usuarioBanco);
        }

        public void AtualizarSenha(Guid id, AtualizarSenhaUsuarioDto dto)
        {
            Usuario usuarioBanco = _repository.BuscarPorId(id);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            dto.SenhaAtual = dto.SenhaAtual.Trim();
            dto.NovaSenha = dto.NovaSenha.Trim();

            _repository.AtualizarSenha(usuarioBanco);
        }

        public void AtualizarStatus(Guid id, AtualizarStatusUsuarioDto dto)
        {
            Usuario usuarioBanco = _repository.BuscarPorId(id);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            usuarioBanco.Ativo = dto.Ativo;
            _repository.AtualizarStatus(usuarioBanco);
        }


    }
}
