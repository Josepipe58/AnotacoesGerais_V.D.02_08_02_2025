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
    public SubcategoriaRepositorio _subcategoriaRepositorio = new();
    public NomeDescricaoRepositorio _nomeDescricaoRepositorio = new();
    public AnotacaoGeralRepositorio _anotacaoGeralRepositorio = new();

    public AnotacaoGeralModel AnotacaoGeralModel { get; set; } = new();
    public CategoriaModel CategoriaModel { get; set; } = new();
    public SubcategoriaModel SubcategoriaModel { get; set; } = new();
    public NomeDescricaoModel NomeDescricaoModel { get; set; } = new();

    private readonly ObservableCollection<AnotacaoGeral> _listaDeAnotacoesGerais = [];
    public ReadOnlyObservableCollection<AnotacaoGeral> ListaDeAnotacoesGerais { get; }

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
                    AtualizarListaDeSubcategorias(CategoriaSelecionada, true);
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
                    AtualizarListaDeNomeDescricao(SubcategoriaSelecionada, true);
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

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private Categoria _categoriaSelecionadaEditar;
    public Categoria CategoriaSelecionadaEditar
    {
        get => _categoriaSelecionadaEditar;
        set
        {
            if (_categoriaSelecionadaEditar != value)
            {
                _categoriaSelecionadaEditar = value;
                OnPropertyChanged(nameof(CategoriaSelecionadaEditar));
                AtualizarListaDeSubcategorias(CategoriaSelecionadaEditar, false);
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private Subcategoria _subcategoriaSelecionadaEditar = new();
    public Subcategoria SubcategoriaSelecionadaEditar
    {
        get => _subcategoriaSelecionadaEditar;
        set
        {
            if (_subcategoriaSelecionadaEditar != value)
            {
                _subcategoriaSelecionadaEditar = value;
                OnPropertyChanged(nameof(SubcategoriaSelecionadaEditar));

                AtualizarListaDeNomeDescricao(SubcategoriaSelecionadaEditar, false);
            }
        }
    }

    // Método interno comum para atualizar a lista de Subcategorias.
    // chamarConsultas = true -> também chama ConsultasDeAnotacoesGerais() (usado no fluxo não editar)
    private void AtualizarListaDeSubcategorias(Categoria categoria, bool chamarConsultas)
    {
        if (categoria != null)
        {
            SubcategoriaModel.ListaDeSubcategorias = [.. SubcategoriaRepositorio.ObterSubcategoriasPorId(categoria.Id) ?? []];
            SubcategoriaModel.IndiceSelecionadoSubcategoria = -1;

            if (chamarConsultas)
                ConsultasDeAnotacoesGerais();
        }
        else
        {
            SubcategoriaModel.ListaDeSubcategorias = [];
        }
    }

    // Método interno comum para atualizar a lista de NomeDescricao.
    // chamarConsultas = true -> também chama ConsultasDeAnotacoesGerais() (usado no fluxo não editar)
    private void AtualizarListaDeNomeDescricao(Subcategoria subcategoria, bool chamarConsultas)
    {
        if (subcategoria != null)
        {
            NomeDescricaoModel.ListaDoNomeDescricao = [.. NomeDescricaoRepositorio.ObterNomeDescricaoPorId(subcategoria.Id) ?? []];

            if (chamarConsultas)
                ConsultasDeAnotacoesGerais();
        }
        else
        {
            NomeDescricaoModel.ListaDoNomeDescricao = [];
        }
    }

    public AnotacaoGeralViewModel()
    {
        //Carregar ComboBox de Categorias de Despesa.
        CategoriaModel.ListaDeCategorias = [.. _categoriaRepositorio.ObterListaDeTodos() ?? []];
        ConsultasDeAnotacoesGerais();
        ContadorDeRegistros();

        //Carregar DataGrid de Anotações Gerais.
        //Usando encapsulamento para obter a lista de Anotações Gerais do repositório e armazená-la em uma coleção observável.
        ListaDeAnotacoesGerais = new ReadOnlyObservableCollection<AnotacaoGeral>(_listaDeAnotacoesGerais);
    }
}
