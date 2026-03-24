using System;
using System.Collections.Generic;

namespace SistemaPatrimonio.Domains;

public partial class StatusTransferencia
{
    public Guid StatusTransferenciaID { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; } = new List<SolicitacaoTransferencia>();
}
