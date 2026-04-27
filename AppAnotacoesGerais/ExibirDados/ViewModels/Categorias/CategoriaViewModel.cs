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

        var listaDeCategorias = _categoriaRepositorio.ObterListaDeTodos().ToList() ?? [];
        //Carregar DataGrid de Categorias.        
        CategoriaModel.ListaDeCategorias = new ObservableCollection<Categoria>(listaDeCategorias);
    }
}
