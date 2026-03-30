using SistemaPatrimonio.Exceptions;

namespace SistemaPatrimonio.Applications.Regras
{
    public class Validar
    {
        public static void ValidarNome(string nome) 
        {
            if (string.IsNullOrEmpty(nome)) 
            {
                throw new DomainException("Nome é obrigatório");
            }
        }
        public static void ValidarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                throw new DomainException("Estado é obrigatório.");
            }
        }
    }
}
