using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.UsuarioDto;
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
    }
}
