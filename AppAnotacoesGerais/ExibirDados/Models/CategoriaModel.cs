using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.ExibirDados.Models;

public class CategoriaModel : ViewModelBase
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

    private int _indiceSelecionadoCategoria;
    public int IndiceSelecionadoCategoria
    {
        get => _indiceSelecionadoCategoria;
        set
        {
            if (_indiceSelecionadoCategoria != value)
            {
                _indiceSelecionadoCategoria = value;
                OnPropertyChanged(nameof(IndiceSelecionadoCategoria));
            }
        }
    }

    //A Lista de Categorias é acessada por várias instancias da CategoriaModel,
    //então é mais eficiente usar um repositório compartilhado para obter os dados.
    //Dessa forma, todas as instâncias da CategoriaModel podem acessar a mesma
    //lista de categorias sem precisar criar uma nova instância do repositório para cada uma.
    public CategoriaRepositorio _categoriaRepositorio = new();
    public ObservableCollection<Categoria> _listaDeCategorias ;
    public ObservableCollection<Categoria> ListaDeCategorias
    {
        get => _listaDeCategorias;
        set
        {
            if (_listaDeCategorias != value)
            {
                _listaDeCategorias = value;
                OnPropertyChanged(nameof(ListaDeCategorias));
            }
        }
    }

    public CategoriaModel()
    {
        //Usando encapsulamento para obter a lista de categorias do repositório e armazená-la em uma coleção observável.       
        _listaDeCategorias = [.. _categoriaRepositorio.ObterListaDeTodos() ?? []];

        ListaDeCategorias = new ObservableCollection<Categoria>(_listaDeCategorias);
    }
}
