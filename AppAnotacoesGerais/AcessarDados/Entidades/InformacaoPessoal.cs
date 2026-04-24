using System.ComponentModel.DataAnnotations;

namespace AppAnotacoesGerais.AcessarDados.Entidades;

public class InformacaoPessoal
{

    [Key]
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Titulo { get; set; }

    [Required, StringLength(maximumLength: 100000)]
    public string Descricao { get; set; }
}
