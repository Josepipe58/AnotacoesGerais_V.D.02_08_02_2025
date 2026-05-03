using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.ExibirDados.Models;

public class NomeDescricaoModel : ViewModelBase
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


    private string _nomeDaDescricao;
    public string NomeDaDescricao
    {
        get => _nomeDaDescricao;
        set
        {
            _nomeDaDescricao = value;
            OnPropertyChanged(nameof(NomeDaDescricao));
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

    private int _subcategoriaId;
    public int SubcategoriaId
    {
        get => _subcategoriaId;
        set
        {
            _subcategoriaId = value;
            OnPropertyChanged(nameof(SubcategoriaId));
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
}
