using AppAnotacoesGerais.ExibirDados.Comandos;

namespace AppAnotacoesGerais.ExibirDados.Models;

public class AnotacaoGeralModel : ViewModelBase
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

    private string _nomeDescricao;
    public string NomeDaDescricao
    {
        get => _nomeDescricao;
        set
        {
            _nomeDescricao = value;
            OnPropertyChanged(nameof(NomeDaDescricao));
        }
    }

    private string _descricao;
    public string Descricao
    {
        get => _descricao;
        set
        {
            _descricao = value;
            OnPropertyChanged(nameof(Descricao));
        }
    }

    private DateTime _data = DateTime.Now;
    public DateTime Data
    {
        get => _data;
        set
        {
            _data = value;
            OnPropertyChanged(nameof(Data));
        }
    }

    //Esse campo é usadado por causa do Dispacher que gera um erro nos ComboBoxes.
    public string _diferenciarMetodos;
    public string DiferenciarMetodos
    {
        get => _diferenciarMetodos;
        set
        {
            if (_diferenciarMetodos != value)
            {
                _diferenciarMetodos = value;
                OnPropertyChanged(nameof(DiferenciarMetodos));
            }
        }
    }
}
