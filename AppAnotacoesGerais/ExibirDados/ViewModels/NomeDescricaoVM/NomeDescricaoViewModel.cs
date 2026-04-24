using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.NomeDescricaoVM;

public partial class NomeDescricaoViewModel : ViewModelBase
{
    private string _textoPesquisa;
    public NomeDescricaoRepositorio _nomeDaDescricaoRepositorio = new();
    private ObservableCollection<NomeDescricao> _listaDoNomeDaDescricao;
    
    public CategoriaModel CategoriaModel { get; set; } = new();
    public SubcategoriaModel SubcategoriaModel { get; set; } = new();
    public NomeDescricaoModel NomeDaDescricaoModel { get; set; } = new();

    public string TextoPesquisa 
    { 
        get => _textoPesquisa; 
        set
        {
            if (_textoPesquisa != value)
            {
                _textoPesquisa = value;
                OnPropertyChanged(nameof(TextoPesquisa));
                PesquisarNomeDaDescricao();
            }
        }
    }

    public ObservableCollection<NomeDescricao> ListaDoNomeDaDescricao
    { 
        get => _listaDoNomeDaDescricao;
        set
        {
            if (_listaDoNomeDaDescricao != value)
            {
                _listaDoNomeDaDescricao = value;
                OnPropertyChanged(nameof(ListaDoNomeDaDescricao));
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Filtrar Categorias e o ComboBox de Categorias.
    private Categoria _categoriaSelecionada;
    public Categoria CategoriaSelecionada
    {
        get => _categoriaSelecionada;
        set
        {
            if (_categoriaSelecionada != value)
            {
                _categoriaSelecionada = value;
                OnPropertyChanged(nameof(CategoriaSelecionada));
                ObterListaDeSubcategorias();

            }
        }
    }

    // Método do evento: atualiza categorias e aplica filtro
    private void ObterListaDeSubcategorias()
    {
        if (CategoriaSelecionada != null)
        {
            SubcategoriaModel.ListaDeSubcategorias = [.. SubcategoriaRepositorio.ObterSubcategoriasPorId(CategoriaSelecionada.Id)];
            SubcategoriaModel.IndiceSelecionadoSubcategoria = 0;
        }
        else
        {
            CategoriaModel.ListaDeCategorias = [];
        }
    }

    private void PesquisarNomeDaDescricao()
    {
        var listaDoNomeDaDescricao = NomeDescricaoRepositorio.ObterNomeDescricao() ?? Enumerable.Empty<NomeDescricao>();

        // Aplica filtro por texto de pesquisa (se informado)
        if (!string.IsNullOrWhiteSpace(TextoPesquisa))
        {
            var busca = TextoPesquisa.Trim();
            listaDoNomeDaDescricao = listaDoNomeDaDescricao.Where(nd => !string.IsNullOrEmpty(nd.NomeDaDescricao) &&
                                      nd.NomeDaDescricao.Contains(busca, StringComparison.OrdinalIgnoreCase));
        }
        ListaDoNomeDaDescricao = [];
        ListaDoNomeDaDescricao = [.. listaDoNomeDaDescricao];
    }

    public NomeDescricaoViewModel()
    {
        TextoPesquisa = string.Empty;
        ListaDoNomeDaDescricao = [];
        ListaDoNomeDaDescricao = [.. NomeDescricaoRepositorio.ObterNomeDescricao()];
    }
}
