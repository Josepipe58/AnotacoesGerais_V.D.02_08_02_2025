using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.NomeDescricaoVM;

public partial class NomeDescricaoViewModel : ViewModelBase
{
    private string _textoPesquisa;
    public NomeDescricaoRepositorio _nomeDaDescricaoRepositorio = new();
    private ObservableCollection<NomeDescricao> _listaDoNomeDescricao;

    public CategoriaModel CategoriaModel { get; set; } = new();
    public SubcategoriaModel SubcategoriaModel { get; set; } = new();
    public NomeDescricaoModel NomeDescricaoModel { get; set; } = new();

    public string TextoPesquisa
    {
        get => _textoPesquisa;
        set
        {
            if (_textoPesquisa != value)
            {
                _textoPesquisa = value;
                OnPropertyChanged(nameof(TextoPesquisa));
                PesquisarNomeDescricao();
            }
        }
    }

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
                ObterListaDeSubcategorias();

            }
        }
    }

    //Método do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
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

    private void PesquisarNomeDescricao()
    {
        var listaDoNomeDescricao = NomeDescricaoRepositorio.ObterNomeDescricao() ?? Enumerable.Empty<NomeDescricao>();

        // Aplica filtro por texto de pesquisa (se informado)
        if (!string.IsNullOrWhiteSpace(TextoPesquisa))
        {
            var busca = TextoPesquisa.Trim();
            listaDoNomeDescricao = listaDoNomeDescricao.Where(nd => !string.IsNullOrEmpty(nd.NomeDaDescricao) &&
                                      nd.NomeDaDescricao.Contains(busca, StringComparison.OrdinalIgnoreCase));
        }
        ListaDoNomeDescricao = [];
        ListaDoNomeDescricao = [.. listaDoNomeDescricao];
    }

    public void LimparDados()
    {
        NomeDescricaoModel.Id = 0;
        NomeDescricaoModel.NomeDaDescricao = null;

        var listaDeSubcategorias = NomeDescricaoRepositorio.ObterNomeDescricao().ToList() ?? [];

        //Carregar DataGrid do Nome da Descrição.        
        ListaDoNomeDescricao = new ObservableCollection<NomeDescricao>(listaDeSubcategorias);
    }

    public NomeDescricaoViewModel()
    {
        TextoPesquisa = string.Empty;
        ListaDoNomeDescricao = [];
        ListaDoNomeDescricao = [.. NomeDescricaoRepositorio.ObterNomeDescricao()];
    }
}
