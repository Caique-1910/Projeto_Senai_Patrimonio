using CsvHelper.Configuration;
using SistemaPatrimonio.DTOs.PatrimonioDto;
namespace SistemaPatrimonio.Applications.Mapeamentos
{
    public class ImportarPatrimonioCsvMap : ClassMap<ImportarPatrimonioCsvDto>
    {
        public ImportarPatrimonioCsvMap() 
        {
            Map(m => m.NumeroPatrimonio).Name("N° invent.");
            Map(m => m.Denominacao).Name("Denominação do imobilizado");
            Map(m => m.DataIncorporacao).Name("Dt.incorp.");
            Map(m => m.ValorAquisicao).Name("ValAquis");
        }
    }
}
