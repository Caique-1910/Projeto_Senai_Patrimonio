using System;
using System.Collections.Generic;

namespace SistemaPatrimonio.Domains;

public partial class Log_Patrimonio
{
    public Guid LogPatrimonioID { get; set; }

    public DateTime DataTransferencia { get; set; }

    public Guid TipoAlteracaoID { get; set; }

    public Guid PatrimonioID { get; set; }

    public Guid UsuarioID { get; set; }

    public Guid LocalID { get; set; }

    public Guid StatusPatrimonioID { get; set; }

    public virtual Local Local { get; set; } = null!;

    public virtual Patrimonio Patrimonio { get; set; } = null!;

    public virtual StatusPatrimonio StatusPatrimonio { get; set; } = null!;

    public virtual TipoAlteracao TipoAlteracao { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
