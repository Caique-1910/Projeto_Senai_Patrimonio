namespace SistemaPatrimonio.DTOs.UsuarioDto
{
    public class ListarUsuarioDto
    {
        public Guid UsuarioID { get; set; }

        public string NIF { get; set; } = null!;

        public string Nome { get; set; } = null!;

        public string? RG { get; set; }

        public string CPF { get; set; } = null!;

        public string CarteiraTrabalho { get; set; } = null!;

        public byte[] Senha { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool? Ativo { get; set; }

        public bool? PrimeiroAcesso { get; set; }
    }
}
