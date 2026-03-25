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
    }
}
