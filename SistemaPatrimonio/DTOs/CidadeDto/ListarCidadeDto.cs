using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.DTOs.CidadeDto
{
    public class ListarCidadeDto
    {
        public Guid CidadeId { get; set; }
        public string NomeCidade { get; set; } = string.Empty;
        public string Estado { get; set; }
    }
}
