using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAnotacoesGerais.AcessarDados.Entidades;

public class ConsumoGas
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "date")]
    public DateTime DataTroca { get; set; }

    public int DiasConsumo { get; set; }

    [Column(TypeName = "date")]
    public DateTime DataCompra { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Preco { get; set; }

    [Required, StringLength(30)]
    public string Fornecedor { get; set; }
}
