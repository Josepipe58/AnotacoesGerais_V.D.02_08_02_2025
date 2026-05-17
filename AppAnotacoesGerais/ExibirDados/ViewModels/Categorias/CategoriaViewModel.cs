using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.Categorias;

public partial class CategoriaViewModel
{
    public CategoriaRepositorio _categoriaRepositorio = new();
    public CategoriaModel CategoriaModel { get; set; } = new();

    public void LimparDados()
    {
        CategoriaModel.Id = 0;
        CategoriaModel.NomeCategoria = null;

        //Carregar DataGrid de Categorias.
        var listaDeCategorias = _categoriaRepositorio.ObterListaDeTodos().ToList() ?? [];                
        CategoriaModel.ListaDeCategorias = new ObservableCollection<Categoria>(listaDeCategorias);
    }
}
