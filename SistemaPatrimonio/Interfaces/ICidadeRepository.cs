using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Interfaces
{
    public interface ICidadeRepository
    {
        List<Cidade> Listar();
        Cidade BuscarPorId(Guid id);
        Cidade BuscarPorNomeEEstado(string nomeCidade, string nomeEstado);
        void Adicionar(Cidade cidade);
        void Atualizar(Cidade cidade);
    }
}
