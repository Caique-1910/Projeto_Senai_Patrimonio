using System;
using System.Collections.Generic;

namespace SistemaPatrimonio.Domains;

public partial class TipoAlteracao
{
    public Guid TipoAlteracaoID { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();
}
