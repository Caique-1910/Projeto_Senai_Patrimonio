using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.EnderecoDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarEnderecoDto> Listar()
        {
            List<Endereco> enderecos = _repository.Listar();

            List<ListarEnderecoDto> enderecosDto = enderecos.Select(endereco => new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Cep = endereco.CEP,
                Complemento = endereco.Complemento,
                BairroID = endereco.BairroID
            }).ToList();

            return enderecosDto;
        }

        public ListarEnderecoDto BuscarPorId(Guid enderecoId)
        {
            Endereco? endereco = _repository.BuscarPorId(enderecoId);

            if (endereco == null)
            {
                throw new DomainException("Endereço não encontrado.");
            }

            return new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Cep = endereco.CEP,
                Complemento = endereco.Complemento,
                BairroID = endereco.BairroID
            };
        }

        public void Adicionar(CriarEnderecoDto dto)
        {
            Validar.ValidarLogradouro(dto.Logradouro);

            Endereco? enderecoExistente = _repository.BuscarPorLogradouroENumero(dto.Logradouro, dto.Numero, dto.BairroID);

            if (enderecoExistente != null)
            {
                throw new DomainException("Já existe um endereço com esse logradouro, número e bairro.");
            }

            if (!_repository.BairroExiste(dto.BairroID))
            {
                throw new DomainException("Bairro não encontrado.");
            }

            Endereco endereco = new Endereco
            {
                Logradouro = dto.Logradouro,
                Numero = dto.Numero,
                CEP = dto.Cep,
                Complemento = dto.Complemento,
                BairroID = dto.BairroID
            };

            _repository.Adicionar(endereco);
        }

        public void Atualizar(Guid id, CriarEnderecoDto dto)
        {
            Validar.ValidarNome(dto.Logradouro);

            Endereco? enderecoExistente = _repository.BuscarPorLogradouroENumero(dto.Logradouro, dto.Numero, dto.BairroID);

            if (enderecoExistente != null)
            {
                throw new DomainException("Já existe um endereço com esse logradouro, número e bairro.");
            }

            if (!_repository.BairroExiste(dto.BairroID))
            {
                throw new DomainException("Bairro não encontrado.");
            }

            Endereco endereco = new Endereco
            {
                Logradouro = dto.Logradouro,
                Numero = dto.Numero,
                CEP = dto.Cep,
                Complemento = dto.Complemento,
                BairroID = dto.BairroID
            };

            _repository.Atualizar(endereco);
        }

     }
}
