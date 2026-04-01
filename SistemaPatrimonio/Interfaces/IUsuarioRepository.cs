using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
        Usuario BuscarPorId(Guid usuarioId);
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        Usuario BuscarPorNome(string nome);
        void AtualizarSenha(Usuario usuario);
        void AtualizarStatus(Usuario usuario);
    }
}
