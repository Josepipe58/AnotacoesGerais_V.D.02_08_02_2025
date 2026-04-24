using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAnotacoesGerais.AcessarDados.Entidades;

public class AnotacaoGeral
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string NomeCategoria { get; set; }

    [Required, StringLength(100)]
    public string NomeSubcategoria { get; set; }

    [Required, StringLength(100)]
    public string NomeDaDescricao { get; set; }

    [Required, StringLength(maximumLength: 100000)]
    public string Descricao { get; set; }

    [Column(TypeName = "date")]
    public DateTime Data { get; set; }
}
