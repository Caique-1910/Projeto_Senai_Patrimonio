using System;
using System.Collections.Generic;

namespace SistemaPatrimonio.Domains;

public partial class Usuario
{
    public Guid UsuarioID { get; set; }

    public string NIF { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string? RG { get; set; }

    public string CPF { get; set; } = null!;

    public string CarteiraTrabalho { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? Ativo { get; set; }

    public Guid EnderecoID { get; set; }

    public Guid CargoID { get; set; }

    public Guid TipoUsuarioID { get; set; }

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual Endereco Endereco { get; set; } = null!;

    public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferenciaUsuarioIDAprovacaoNavigation { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferenciaUsuarioIDSolicitacaoNavigation { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual TipoUsuario TipoUsuario { get; set; } = null!;

    public virtual ICollection<Local> LocalLocalUsuario { get; set; } = new List<Local>();
}
