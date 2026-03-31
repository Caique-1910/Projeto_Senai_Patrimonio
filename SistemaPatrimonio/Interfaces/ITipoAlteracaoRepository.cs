using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface ITipoAlteracaoRepository
    {
        public List<TipoAlteracao> Listar();
        TipoAlteracao BuscarPorId(Guid tipoAlteracaoId);
        void Adicionar(TipoAlteracao tipoAlteracao);
        void Atualizar(TipoAlteracao tipoAlteracao);
        TipoAlteracao BuscarPorNome(string nomeTipoAlteracao);
    }
}
