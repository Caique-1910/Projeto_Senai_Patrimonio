using CsvHelper;
using CsvHelper.Configuration;
using SistemaPatrimonio.Applications.Regras;
using SistemaPatrimonio.Domains;
using SistemaPatrimonio.DTOs.PatrimonioDto;
using SistemaPatrimonio.Exceptions;
using SistemaPatrimonio.Interfaces;
using System.Globalization;
using SistemaPatrimonio.Applications.Mapeamentos;
using Microsoft.AspNetCore.Mvc.ApplicationModels;


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

            List<ListarPatrimonioDto> patrimonioDtos = patrimonios.Select(p => new ListarPatrimonioDto
            {
                PatrimonioID = p.StatusPatrimonioID,
                Denominacao =p.Denominacao,
                NumeroPatrimonio = p.NumeroPatrimonio,
                Valor = p.Valor,
                Imagem = p.Imagem,
                LocalID = p.LocalID,
                StatusPatrimonioID = p.StatusPatrimonioID
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
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            };
        }

        public void Adicionar(IFormFile arquivoCsv , Guid usuarioId)
        {
            if (arquivoCsv == null || arquivoCsv.Length == 0)
            {
                throw new DomainException("Arquivo CSV é obrigatório.");
            }

            Local localSemLocal = _repository.BuscarLocalPorNome("Sem local");

            if (localSemLocal == null)
            {
                throw new DomainException("Localização 'Sem local' não cadastrada.");
            }

            StatusPatrimonio statusAtivo = _repository.BuscarStatusPatrimonioPorNome("Ativo");

            if (statusAtivo == null)
            {
                throw new DomainException("Status 'Ativo' não cadastrado.");
            }

            TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Atualização de dados");

            if (tipoAlteracao == null)
            {
                throw new DomainException("Tipo de alteração 'Atualização de dados' não cadastrado.");
            }

            List<ImportarPatrimonioCsvDto> registros;

            using (var stream = arquivoCsv.OpenReadStream())

            using (var reader = new StreamReader(stream))

            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";" ,

                HeaderValidated = null,

                MissingFieldFound = null,

                BadDataFound = null,

                TrimOptions = TrimOptions.Trim
            }))
            {
                csv.Context.RegisterClassMap<ImportarPatrimonioCsvMap>();

                registros = csv.GetRecords<ImportarPatrimonioCsvDto>().ToList();
            }
            
            var erros = new List<string>();

            foreach (var item in registros)
            {
                if(string.IsNullOrWhiteSpace(item.NumeroPatrimonio))
                {
                    continue; 
                }

                string numeroPatrimonio = item.NumeroPatrimonio.Trim();

                if(string.IsNullOrWhiteSpace(item.Denominacao))
                {
                    erros.Add($"Patrimonio {numeroPatrimonio} sem denominação");
                }

                string denominacao = item.Denominacao.Trim();

                DateTime? dataIncorporacao = null;

                if(!string.IsNullOrWhiteSpace(item.DataIncorporacao))
                {
                    if(DateTime.TryParse(item.DataIncorporacao, new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime dataConvertida))
                    {
                        dataIncorporacao = dataConvertida;
                    }
                }

                decimal? valorAquisicao = null;

                if(!string.IsNullOrWhiteSpace(item.ValorAquisicao))
                {
                    string valorTexto = item.ValorAquisicao.Replace(".", "").Replace(",", ".");

                    if(decimal.TryParse(valorTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorCovertido))
                    {
                       valorAquisicao = valorCovertido; 
                    }

                    Validar.ValidarNumeroPatrimonio(numeroPatrimonio);
                    Validar.ValidarNome(denominacao);

                    bool patrimonioExistente = _repository.BuscarPorNumeroPatrimonio(numeroPatrimonio);

                    if (patrimonioExistente == true)
                    {
                        continue;
                    }

                    Patrimonio patrimonio = new Patrimonio
                    {
                        Denominacao = denominacao,
                        NumeroPatrimonio = numeroPatrimonio,
                        Valor = valorAquisicao,
                        Imagem = null,
                        LocalID = localSemLocal.LocalID,
                        StatusPatrimonioID = statusAtivo.StatusPatrimonioID
                    };

                    _repository.Adicionar(patrimonio);

                    Log_Patrimonio log = new Log_Patrimonio
                    {
                        DataTransferencia = dataIncorporacao ?? DateTime.Now,
                        TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                        StatusPatrimonioID = patrimonio.StatusPatrimonioID,
                        PatrimonioID = patrimonio.PatrimonioID,
                        UsuarioID = usuarioId,
                        LocalID = patrimonio.LocalID
                    };

                    _repository.AdicionarLog(log);
                }

            }
        }

        public void AtualizarStatus(Guid patrimonioId, AtualizarStatusPatrimonioDto dto)
        {
            Patrimonio patrimonioBanco = _repository.BuscarPorId(patrimonioId);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.StatusPatrimonioExiste(dto.StatusPatrimonioID))
            {
                throw new DomainException("Status de patrimônio informado não existe.");
            }

            patrimonioBanco.StatusPatrimonioID = dto.StatusPatrimonioID;

            _repository.AtualizarStatus(patrimonioBanco);
        }
    }
}
