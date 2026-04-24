using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAnotacoesGerais.AcessarDados.Entidades;

public class NomeDescricao
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string NomeDaDescricao { get; set; }

    [ForeignKey(nameof(CategoriaId))]
    public int CategoriaId { get; set; }

    [ForeignKey(nameof(SubcategoriaId))]
    public int SubcategoriaId { get; set; }

    [NotMapped]
    public string NomeCategoria { get; set; }

    [NotMapped]
    public string NomeSubcategoria { get; set; }

    public virtual Categoria Categoria { get; set; }
    public virtual Subcategoria Subcategoria { get; set; }
}
