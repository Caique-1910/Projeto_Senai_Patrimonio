namespace SistemaPatrimonio.DTOs.LocalDto
{
    public class CriarLocalDto
    {
        public string NomeLocal { get; set; } = string.Empty;
        public int localSap {  get; set; }
        public string descricaoSap { get; set; }
        public Guid areaID { get; set; }
    }
}
