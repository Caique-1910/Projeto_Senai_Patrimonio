using System;
using System.Collections.Generic;

namespace SistemaPatrimonio.Domains;

public partial class StatusPatrimonio
{
    public Guid StatusPatrimonioID { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; } = new List<SolicitacaoTransferencia>();
}
