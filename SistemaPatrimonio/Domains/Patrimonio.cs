using System;
using System.Collections.Generic;

namespace SistemaPatrimonio.Domains;

public partial class Patrimonio
{
    public Guid PatrimonioID { get; set; }

    public string Denominacao { get; set; } = null!;

    public string NumeroPatrimonio { get; set; } = null!;

    public decimal? Valor { get; set; }

    public string Imagem { get; set; } = null!;

    public Guid LocalID { get; set; }

    public Guid TipoPatrimonioID { get; set; }

    public Guid StatusPatrimonioID { get; set; }

    public virtual Local Local { get; set; } = null!;

    public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual StatusPatrimonio StatusPatrimonio { get; set; } = null!;

    public virtual TipoPatrimonio TipoPatrimonio { get; set; } = null!;
}
