using AppAnotacoesGerais.AcessarDados.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AppAnotacoesGerais.AcessarDados;

public class Contexto : DbContext
{
    public Contexto() { }

    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public virtual DbSet<AnotacaoGeral> TAnotacaoGeral { get; set; }
    public virtual DbSet<Categoria> TCategoria { get; set; }
    public virtual DbSet<ConsumoGas> TConsumoGas { get; set; }
    public virtual DbSet<InformacaoPessoal> TInformacaoPessoal { get; set; }
    public virtual DbSet<NomeDescricao> TNomeDescricao { get; set; }
    public virtual DbSet<Subcategoria> TSubcategoria { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //AnotacoesGerais
        optionsBuilder.UseSqlServer(" Data Source = JOSEPIPE-PC\\FINANCEIRO; Initial Catalog = BancoDeTestesAnotacoesGerais; " +
                "Integrated Security = True; TrustServerCertificate=True");
    }
}
