using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Windows;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.AnotacoesGerais;

public partial class AnotacaoGeralViewModel : ViewModelBase
{
    public CategoriaRepositorio _categoriaRepositorio = new();
    public AnotacaoGeralRepositorio _anotacaoGeralRepositorio = new();

    public CategoriaModel CategoriaModel { get; set; } = new();
    public SubcategoriaModel SubcategoriaModel { get; set; } = new();
    public AnotacaoGeralModel AnotacaoGeralModel { get; set; } = new();

    private readonly ObservableCollection<AnotacaoGeral> _listaDeAnotacoesGerais = [];
    public ReadOnlyObservableCollection<AnotacaoGeral> ListaDeAnotacoesGerais { get; }

    private ObservableCollection<NomeDescricao> _listaDoNomeDescricao;
    public ObservableCollection<NomeDescricao> ListaDoNomeDescricao
    {
        get => _listaDoNomeDescricao;
        set
        {
            if (_listaDoNomeDescricao != value)
            {
                _listaDoNomeDescricao = value;
                OnPropertyChanged(nameof(ListaDoNomeDescricao));
            }
        }
    }

    private int _indiceSelecionadoNomeDescricao;
    public int IndiceSelecionadoNomeDescricao
    {
        get => _indiceSelecionadoNomeDescricao;
        set
        {
            if (_indiceSelecionadoNomeDescricao != value)
            {
                _indiceSelecionadoNomeDescricao = value;
                OnPropertyChanged(nameof(IndiceSelecionadoNomeDescricao));
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
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

                //Aguarde o binding ser atualizado antes de chamar os métodos.
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ObterListaDeSubcategorias();
                    ObterListaDeNomeDescricao();
                });
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private Subcategoria _subcategoriaSelecionada = new();
    public Subcategoria SubcategoriaSelecionada
    {
        get => _subcategoriaSelecionada;
        set
        {
            if (_subcategoriaSelecionada != value)
            {
                _subcategoriaSelecionada = value;
                OnPropertyChanged(nameof(SubcategoriaSelecionada));

                //Aguarde o binding ser atualizado antes de chamar os métodos.
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ObterListaDeNomeDescricao();
                    ConsultasDeAnotacoesGerais();
                });
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Subcategorias e o ComboBox de NomeDescricao.
    private NomeDescricao _nomeDescricaoSelecionada = new();
    public NomeDescricao NomeDescricaoSelecionada
    {
        get => _nomeDescricaoSelecionada;
        set
        {
            if (_nomeDescricaoSelecionada != value)
            {
                _nomeDescricaoSelecionada = value;
                OnPropertyChanged(nameof(NomeDescricaoSelecionada));

                //Aguarde o binding ser atualizado antes de chamar os métodos.
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ConsultasDeAnotacoesGerais();
                });
            }
        }
    }

    //Método do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private void ObterListaDeSubcategorias()
    {
        if (CategoriaSelecionada != null)
        {
            SubcategoriaModel.ListaDeSubcategorias = [.. SubcategoriaRepositorio.ObterSubcategoriasPorId(CategoriaSelecionada.Id) ?? []];
            SubcategoriaModel.IndiceSelecionadoSubcategoria = -1;           
        }
        else
        {
            SubcategoriaModel.ListaDeSubcategorias = [];
        }
    }

    //Método do evento: "SelectionChanged" entre o ComboBox de Sucategorias e o ComboBox de NomeDescricao.
    private void ObterListaDeNomeDescricao()
    {
        if (SubcategoriaSelecionada != null)
        {
            ListaDoNomeDescricao = [.. NomeDescricaoRepositorio.ObterNomeDescricaoPorId(SubcategoriaSelecionada.Id) ?? []];
            IndiceSelecionadoNomeDescricao = -1;
            ConsultasDeAnotacoesGerais();
        }
        else
        {
            ListaDoNomeDescricao = [];
        }
    }

    public AnotacaoGeralViewModel()
    {
        //Carregar ComboBox de Categorias de Despesa.
        CategoriaModel.ListaDeCategorias = [.. _categoriaRepositorio.ObterListaDeTodos() ?? []];
        ConsultasDeAnotacoesGerais();

        //Carregar DataGrid de Anotações Gerais.
        //Usando encapsulamento para obter a lista de Anotações Gerais do repositório e armazená-la em uma coleção observável.
        ListaDeAnotacoesGerais = new ReadOnlyObservableCollection<AnotacaoGeral>(_listaDeAnotacoesGerais);
    }
}
