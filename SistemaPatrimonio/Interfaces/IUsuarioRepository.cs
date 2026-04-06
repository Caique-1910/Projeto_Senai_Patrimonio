using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
        Usuario BuscarPorId(Guid usuarioId);
        Usuario BuscarDuplicado(string nif, string cpf, string email, Guid? usuarioId = null);
        Usuario ObterPorNifComTipoUsuario(string nif);
        bool EnderecoExiste(Guid enderecoId);
        bool CargoExiste(Guid cargoId);
        bool TipoUsuarioExiste(Guid tipoUsuarioId);
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void AtualizarSenha(Usuario usuario);
        void AtualizarStatus(Usuario usuario);
        void AtualizarPrimeiroAcesso(Usuario usuario);
    }
}
