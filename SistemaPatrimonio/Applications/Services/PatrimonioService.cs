using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.PatrimonioDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;

namespace SistemaPatrimonio.Applications.Services
{
    public class PatrimonioService
    {
        private readonly IPatrimonioRepository _repository;

        public PatrimonioService(IPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarPatrimonioDto> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();

            List<ListarPatrimonioDto> patrimonioDtos = patrimonios.Select(sp => new ListarPatrimonioDto
            {
                PatrimonioID = sp.StatusPatrimonioID,
                Denominacao = sp.Denominacao,
                NumeroPatrimonio = sp.NumeroPatrimonio,
                Valor = sp.Valor,
                Imagem = sp.Imagem,
                LocalID = sp.LocalID,
                TipoPatrimonioID = sp.TipoPatrimonioID,
                StatusPatrimonioID = sp.StatusPatrimonioID
            }).ToList();

            return patrimonioDtos;
        }

        public ListarPatrimonioDto BuscarPorId(Guid patrimonioId)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(patrimonioId);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado");
            }

            return new ListarPatrimonioDto
            {
                PatrimonioID = patrimonio.PatrimonioID,
                Denominacao = patrimonio.Denominacao,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalID = patrimonio.LocalID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            };
        }

        public void Adicionar(CriarPatrimonioDto dto)
        {
            Patrimonio patrimonioExistente = _repository.BuscarPorNome(dto.Denominacao);

            if (patrimonioExistente != null)
            {
                throw new DomainException("Já existe um patrimônio com esse número.");
            }

            Patrimonio patrimonio = new Patrimonio
            {
                PatrimonioID = Guid.NewGuid(),
                Denominacao = dto.Denominacao,
                NumeroPatrimonio = dto.NumeroPatrimonio,
                Valor = dto.Valor,
                Imagem = dto.Imagem,
                LocalID = dto.LocalID,
                TipoPatrimonioID = dto.TipoPatrimonioID,
                StatusPatrimonioID = dto.StatusPatrimonioID
            };

            _repository.Adicionar(patrimonio);
        }

        public void Atualizar(Guid patrimonioId, CriarPatrimonioDto dto)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(patrimonioId);

            if (patrimonio == null)
            {

                throw new DomainException("Patrimônio não encontrado");
            }
            Patrimonio patrimonioComMesmoNumero = _repository.BuscarPorNome(dto.Denominacao);

            if (patrimonioComMesmoNumero != null && patrimonioComMesmoNumero.PatrimonioID != patrimonioId)
            {
                throw new DomainException("Já existe um patrimônio com esse número.");
            }

            patrimonio.Denominacao = dto.Denominacao;
            patrimonio.NumeroPatrimonio = dto.NumeroPatrimonio;
            patrimonio.Valor = dto.Valor;
            patrimonio.Imagem = dto.Imagem;
            patrimonio.LocalID = dto.LocalID;
            patrimonio.TipoPatrimonioID = dto.TipoPatrimonioID;
            patrimonio.StatusPatrimonioID = dto.StatusPatrimonioID;

            _repository.Atualizar(patrimonio);
        }

            public void AtualizarStatus(Guid patrimonioId, AtualizarStatusPatrimonioDto dto)
            {
                Patrimonio patrimonio = _repository.BuscarPorId(patrimonioId);
    
                if (patrimonio == null)
                {
                    throw new DomainException("Patrimônio não encontrado");
                }
    
                patrimonio.StatusPatrimonioID = dto.StatusPatrimonioID;
    
                _repository.AtualizarStatus(patrimonio);
        }
    }
}
