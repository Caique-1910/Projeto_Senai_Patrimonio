using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaPatrimonio.Domains;

namespace SistemaPatrimonio.Contexts;

public partial class SistemaPatrimonioContext : DbContext
{
    public SistemaPatrimonioContext()
    {
    }

    public SistemaPatrimonioContext(DbContextOptions<SistemaPatrimonioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Area { get; set; }

    public virtual DbSet<Bairro> Bairro { get; set; }

    public virtual DbSet<Cargo> Cargo { get; set; }

    public virtual DbSet<Cidade> Cidade { get; set; }

    public virtual DbSet<Endereco> Endereco { get; set; }

    public virtual DbSet<Local> Local { get; set; }

    public virtual DbSet<Log_Patrimonio> Log_Patrimonio { get; set; }

    public virtual DbSet<Patrimonio> Patrimonio { get; set; }

    public virtual DbSet<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; }

    public virtual DbSet<StatusPatrimonio> StatusPatrimonio { get; set; }

    public virtual DbSet<StatusTransferencia> StatusTransferencia { get; set; }

    public virtual DbSet<TipoAlteracao> TipoAlteracao { get; set; }

    public virtual DbSet<TipoPatrimonio> TipoPatrimonio { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaID).HasName("PK__Area__70B82028AEC2909B");

            entity.HasIndex(e => e.NomeArea, "UQ__Area__9A7797600211299E").IsUnique();

            entity.Property(e => e.AreaID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeArea)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Bairro>(entity =>
        {
            entity.HasKey(e => e.BairroID).HasName("PK__Bairro__4A093623C26BACD6");

            entity.Property(e => e.BairroID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeBairro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Cidade).WithMany(p => p.Bairro)
                .HasForeignKey(d => d.CidadeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bairro__CidadeID__619B8048");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoID).HasName("PK__Cargo__B4E665EDF05A3BFB");

            entity.HasIndex(e => e.NomeCargo, "UQ__Cargo__4D9FD7DEC6AC6EED").IsUnique();

            entity.Property(e => e.CargoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeCargo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(e => e.CidadeID).HasName("PK__Cidade__B6800959E46AF626");

            entity.Property(e => e.CidadeID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomeCidade)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.EnderecoID).HasName("PK__Endereco__B9D9462FB93C472F");

            entity.Property(e => e.EnderecoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CEP)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Complemento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Logradouro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Bairro).WithMany(p => p.Endereco)
                .HasForeignKey(d => d.BairroID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Endereco__Bairro__656C112C");
        });

        modelBuilder.Entity<Local>(entity =>
        {
            entity.HasKey(e => e.LocalID).HasName("PK__Local__499359DB798223E6");

            entity.ToTable(tb => tb.HasTrigger("trg_Local_SoftDelete"));

            entity.Property(e => e.LocalID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Area).WithMany(p => p.Local)
                .HasForeignKey(d => d.AreaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Local__AreaID__7D439ABD");
        });

        modelBuilder.Entity<Log_Patrimonio>(entity =>
        {
            entity.HasKey(e => e.LogPatrimonioID).HasName("PK__Log_Patr__E716D12BA2704421");

            entity.Property(e => e.LogPatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataTransferencia).HasPrecision(0);

            entity.HasOne(d => d.Local).WithMany(p => p.Log_Patrimonio)
                .HasForeignKey(d => d.LocalID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Log_Patri__Local__17036CC0");

            entity.HasOne(d => d.Patrimonio).WithMany(p => p.Log_Patrimonio)
                .HasForeignKey(d => d.PatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Log_Patri__Patri__151B244E");

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.Log_Patrimonio)
                .HasForeignKey(d => d.StatusPatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Log_Patri__Statu__17F790F9");

            entity.HasOne(d => d.TipoAlteracao).WithMany(p => p.Log_Patrimonio)
                .HasForeignKey(d => d.TipoAlteracaoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Log_Patri__TipoA__14270015");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Log_Patrimonio)
                .HasForeignKey(d => d.UsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Log_Patri__Usuar__160F4887");
        });

        modelBuilder.Entity<Patrimonio>(entity =>
        {
            entity.HasKey(e => e.PatrimonioID).HasName("PK__Patrimon__C5A60BDE3FEC1969");

            entity.ToTable(tb => tb.HasTrigger("trg_Patrimonio_SoftDelete"));

            entity.HasIndex(e => e.NumeroPatrimonio, "UQ__Patrimon__3BC8B35DF2909F74").IsUnique();

            entity.Property(e => e.PatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Denominacao).IsUnicode(false);
            entity.Property(e => e.Imagem).IsUnicode(false);
            entity.Property(e => e.NumeroPatrimonio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Local).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.LocalID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patrimoni__Local__05D8E0BE");

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.StatusPatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patrimoni__Statu__07C12930");

            entity.HasOne(d => d.TipoPatrimonio).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.TipoPatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patrimoni__TipoP__06CD04F7");
        });

        modelBuilder.Entity<SolicitacaoTransferencia>(entity =>
        {
            entity.HasKey(e => e.TransferenciaID).HasName("PK__Solicita__E5B4F5F26373DD45");

            entity.Property(e => e.TransferenciaID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataCriacaoSolicitante).HasPrecision(0);
            entity.Property(e => e.DataResposta).HasPrecision(0);
            entity.Property(e => e.Justificativa).IsUnicode(false);

            entity.HasOne(d => d.Local).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.LocalID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitac__Local__10566F31");

            entity.HasOne(d => d.Patrimonio).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.PatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitac__Patri__0B91BA14");

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.StatusPatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitac__Statu__0D7A0286");

            entity.HasOne(d => d.StatusTransferencia).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.StatusTransferenciaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitac__Statu__0C85DE4D");

            entity.HasOne(d => d.UsuarioIDAprovacaoNavigation).WithMany(p => p.SolicitacaoTransferenciaUsuarioIDAprovacaoNavigation)
                .HasForeignKey(d => d.UsuarioIDAprovacao)
                .HasConstraintName("FK__Solicitac__Usuar__0F624AF8");

            entity.HasOne(d => d.UsuarioIDSolicitacaoNavigation).WithMany(p => p.SolicitacaoTransferenciaUsuarioIDSolicitacaoNavigation)
                .HasForeignKey(d => d.UsuarioIDSolicitacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitac__Usuar__0E6E26BF");
        });

        modelBuilder.Entity<StatusPatrimonio>(entity =>
        {
            entity.HasKey(e => e.StatusPatrimonioID).HasName("PK__StatusPa__B3F336096D803549");

            entity.HasIndex(e => e.Status, "UQ__StatusPa__3A15923FD8388741").IsUnique();

            entity.Property(e => e.StatusPatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusTransferencia>(entity =>
        {
            entity.HasKey(e => e.StatusTransferenciaID).HasName("PK__StatusTr__7AA828B9FFF7FA4A");

            entity.HasIndex(e => e.Status, "UQ__StatusTr__3A15923F595E2467").IsUnique();

            entity.Property(e => e.StatusTransferenciaID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoAlteracao>(entity =>
        {
            entity.HasKey(e => e.TipoAlteracaoID).HasName("PK__TipoAlte__9BEF4F0DD2E8A291");

            entity.HasIndex(e => e.Tipo, "UQ__TipoAlte__8E762CB49C06DDED").IsUnique();

            entity.Property(e => e.TipoAlteracaoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoPatrimonio>(entity =>
        {
            entity.HasKey(e => e.TipoPatrimonioID).HasName("PK__TipoPatr__4DC9FF99F64AA9B7");

            entity.HasIndex(e => e.NomeTipo, "UQ__TipoPatr__7859A10A8F1DF86B").IsUnique();

            entity.Property(e => e.TipoPatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioID).HasName("PK__TipoUsua__7F22C7028399A32A");

            entity.HasIndex(e => e.NomeTipo, "UQ__TipoUsua__7859A10A0D4778B2").IsUnique();

            entity.Property(e => e.TipoUsuarioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE798269CD659");

            entity.ToTable(tb => tb.HasTrigger("trg_Usuario_SoftDelete"));

            entity.HasIndex(e => e.RG, "UQ__Usuario__321537C8EE7C9209").IsUnique();

            entity.HasIndex(e => e.CarteiraTrabalho, "UQ__Usuario__6E25BCA2C2F9B503").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053417428753").IsUnique();

            entity.HasIndex(e => e.CPF, "UQ__Usuario__C1F897312A5E2DD1").IsUnique();

            entity.HasIndex(e => e.NIF, "UQ__Usuario__C7DEC330BB1C0B3F").IsUnique();

            entity.Property(e => e.UsuarioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.CPF)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.CarteiraTrabalho)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NIF)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PrimeiroAcesso).HasDefaultValue(true);
            entity.Property(e => e.RG)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);

            entity.HasOne(d => d.Cargo).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.CargoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__CargoID__778AC167");

            entity.HasOne(d => d.Endereco).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.EnderecoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__Enderec__76969D2E");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.TipoUsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__TipoUsu__787EE5A0");

            entity.HasMany(d => d.LocalLocalUsuario).WithMany(p => p.Usuario)
                .UsingEntity<Dictionary<string, object>>(
                    "LocalUsuario",
                    r => r.HasOne<Local>().WithMany()
                        .HasForeignKey("LocalLocalUsuarioID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Local"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Usuario"),
                    j =>
                    {
                        j.HasKey("UsuarioID", "LocalLocalUsuarioID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
