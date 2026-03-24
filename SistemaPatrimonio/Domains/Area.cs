using System;
using System.Collections.Generic;

namespace SistemaPatrimonio.Domains;

public partial class Area
{
    public Guid AreaID { get; set; }

    public string NomeArea { get; set; } = null!;

    public virtual ICollection<Local> Local { get; set; } = new List<Local>();
}
