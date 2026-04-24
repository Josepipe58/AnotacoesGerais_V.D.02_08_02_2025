using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados.Repositorios;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.Categorias;

public partial class CategoriaViewModel
{
    public CategoriaRepositorio _categoriaRepositorio = new();
    public CategoriaModel CategoriaModel { get; set; } = new();
}
