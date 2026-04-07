using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Identity.Client;

namespace SistemaPatrimonio.DTOs.LogPatrimonioDto
{
    public class ListarLogPatrimonioDto
    {
        public Guid LogPatrimonioID { get; set; }
        public DateTime DataTransferencia { get; set; }
        public Guid PatrimonioID { get; set; }
        public string DenomoinacaoPatrimonio { get; set; } = string.Empty;
        public string TipoAlteracao { get; set; } = string.Empty;
        public string StautusPatrimonio { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
    }
}
