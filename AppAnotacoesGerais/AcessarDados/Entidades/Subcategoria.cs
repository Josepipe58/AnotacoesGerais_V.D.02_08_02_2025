using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAnotacoesGerais.AcessarDados.Entidades;

public class Subcategoria
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string NomeSubcategoria { get; set; }

    [ForeignKey(nameof(CategoriaId))]
    public int CategoriaId { get; set; }

    [NotMapped]
    public string NomeCategoria { get; set; }

    public virtual Categoria Categoria { get; set; }
}
