using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.ExibirDados.Models;

public class SubcategoriaModel : ViewModelBase
{
    private int _id;
    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    private string _nomeSubcategoria;
    public string NomeSubcategoria
    {
        get => _nomeSubcategoria;
        set
        {
            _nomeSubcategoria = value;
            OnPropertyChanged(nameof(NomeSubcategoria));
        }
    }

    private int _categoriaId;
    public int CategoriaId
    {
        get => _categoriaId;
        set
        {
            _categoriaId = value;
            OnPropertyChanged(nameof(CategoriaId));
        }
    }

    //Essas propriedades são usadas como objeto de transferência ou variáveis.
    private string _nomeCategoria;
    public string NomeCategoria
    {
        get => _nomeCategoria;
        set
        {
            _nomeCategoria = value;
            OnPropertyChanged(nameof(NomeCategoria));
        }
    }
    
    //Essas propriedades são usadas em consultas, Despesa, Poupança, Receita e Investimento.
    private int _indiceSelecionadoSubcategoria;
    public int IndiceSelecionadoSubcategoria
    {
        get => _indiceSelecionadoSubcategoria;
        set
        {
            if (_indiceSelecionadoSubcategoria != value)
            {
                _indiceSelecionadoSubcategoria = value;
                OnPropertyChanged(nameof(IndiceSelecionadoSubcategoria));
            }
        }
    }

    //A Lista de Subcategorias por Id é acessada por várias instancias da SubcategoriaModel,
    //então é mais eficiente usar um repositório compartilhado para obter os dados.
    //Dessa forma, todas as instâncias da SubcategoriaModel podem acessar a mesma
    //lista de Subcategorias sem precisar criar uma nova instância do repositório para cada uma.
    public SubcategoriaRepositorio _subcategoriaRepositorio = new();

    private readonly ObservableCollection<Subcategoria> _listaDeSubcategoriasPorId = [];
    public ObservableCollection<Subcategoria> ListaDeSubcategoriasPorId { get; set; }


    private ObservableCollection<Subcategoria> _listaDeSubcategorias;
    public ObservableCollection<Subcategoria> ListaDeSubcategorias
    {
        get => _listaDeSubcategorias;
        set
        {
            if (_listaDeSubcategorias != value)
            {
                _listaDeSubcategorias = value;
                OnPropertyChanged(nameof(ListaDeSubcategorias));
            }
        }
    }

    public SubcategoriaModel()
    {
        //Usando encapsulamento para obter a lista de Subcategorias do repositório e armazená-la em uma coleção observável.       
        //_listaDeSubcategoriasPorId = [.. SubcategoriaRepositorio.ObterSubcategoriasPorId(CategoriaId) ?? []];

        //ListaDeSubcategoriasPorId = new ObservableCollection<Subcategoria>(_listaDeSubcategoriasPorId);


        //_listaDeSubcategorias = [.. SubcategoriaRepositorio.ObterSubcategorias() ?? []];

        //ListaDeSubcategorias = new ObservableCollection<Subcategoria>(_listaDeSubcategorias);

    }
}
