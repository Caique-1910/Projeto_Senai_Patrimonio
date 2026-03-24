using System;
using System.Collections.Generic;

namespace SistemaPatrimonio.Domains;

public partial class Local
{
    public Guid LocalID { get; set; }

    public string Nome { get; set; } = null!;

    public int? LocalSap { get; set; }

    public string? Descricao { get; set; }

    public bool? Ativo { get; set; }

    public Guid AreaID { get; set; }

    public virtual Area Area { get; set; } = null!;

    public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
