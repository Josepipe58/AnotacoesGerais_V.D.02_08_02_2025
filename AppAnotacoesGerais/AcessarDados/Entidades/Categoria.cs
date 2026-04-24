using System.ComponentModel.DataAnnotations;

namespace AppAnotacoesGerais.AcessarDados.Entidades;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string NomeCategoria { get; set; }
}
