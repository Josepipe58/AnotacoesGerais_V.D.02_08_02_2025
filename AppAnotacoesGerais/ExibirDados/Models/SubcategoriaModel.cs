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
}
