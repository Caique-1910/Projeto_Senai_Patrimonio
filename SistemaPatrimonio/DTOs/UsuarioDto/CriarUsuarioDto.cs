namespace SistemaPatrimonio.DTOs.UsuarioDto
{
    public class CriarUsuarioDto
    {
        public string NIF { get; set; } = null!;

        public string Nome { get; set; } = null!;

        public string? RG { get; set; }

        public string CPF { get; set; } = null!;

        public string CarteiraTrabalho { get; set; } = null!;

        public byte[] Senha { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
