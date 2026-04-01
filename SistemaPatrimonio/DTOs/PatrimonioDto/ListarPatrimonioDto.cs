namespace SistemaPatrimonio.DTOs.PatrimonioDto
{
    public class ListarPatrimonioDto
    {
        public Guid PatrimonioID { get; set; }

        public string Denominacao { get; set; } = null!;

        public string NumeroPatrimonio { get; set; } = null!;

        public decimal? Valor { get; set; }

        public string Imagem { get; set; } = null!;

        public Guid LocalID { get; set; }

        public Guid TipoPatrimonioID { get; set; }

        public Guid StatusPatrimonioID { get; set; }
    }
}
