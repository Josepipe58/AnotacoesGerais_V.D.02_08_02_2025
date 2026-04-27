using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.Subcategorias;

public partial class SubcategoriaViewModel : ViewModelBase
{
    public SubcategoriaRepositorio _subcategoriaRepositorio = new();
    public CategoriaRepositorio _categoriaRepositorio = new();

    public CategoriaModel CategoriaModel { get; set; } = new();
    public SubcategoriaModel SubcategoriaModel { get; set; } = new();

    private string _textoPesquisa;
    public string TextoPesquisa
    {
        get => _textoPesquisa;
        set
        {
            if (_textoPesquisa != value)
            {
                _textoPesquisa = value;
                OnPropertyChanged(nameof(TextoPesquisa));
                PesquisarSubcategorias();
            }
        }
    }

    private void PesquisarSubcategorias()
    {
        var listaDeSubcategorias = SubcategoriaRepositorio.ObterSubcategorias() ?? Enumerable.Empty<Subcategoria>();

        // Aplica filtro por texto de pesquisa (se informado)
        if (!string.IsNullOrWhiteSpace(TextoPesquisa))
        {
            var busca = TextoPesquisa.Trim();
            listaDeSubcategorias = listaDeSubcategorias.Where(sc => !string.IsNullOrEmpty(sc.NomeSubcategoria) &&
                                      sc.NomeSubcategoria.Contains(busca, StringComparison.OrdinalIgnoreCase));
        }
        SubcategoriaModel.ListaDeSubcategorias = [];
        SubcategoriaModel.ListaDeSubcategorias = [.. listaDeSubcategorias];
    }

    public void LimparDados()
    {
        SubcategoriaModel.Id = 0;
        SubcategoriaModel.NomeSubcategoria = null;

        var listaDeSubcategorias = SubcategoriaRepositorio.ObterSubcategorias().ToList() ?? [];

        //Carregar DataGrid de Subcategorias.        
        SubcategoriaModel.ListaDeSubcategorias = new ObservableCollection<Subcategoria>(listaDeSubcategorias);
    }

    public SubcategoriaViewModel()
    {
        TextoPesquisa = string.Empty;
    }
}
