namespace SistemaPatrimonio.DTOs.LocalDto
{
    public class ListarLocalDto
    {
        public Guid localID { get; set; }
        public string nomeLocal {  get; set; } = string.Empty;
        public int? localSap {  get; set; }
        public string descricaoSap { get; set; }
        public Guid areaID { get; set; }
    }
}
