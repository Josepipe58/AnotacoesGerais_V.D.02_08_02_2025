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

    private int _contadorRegistros;
    public int ContadorRegistros
    {
        get => _contadorRegistros;
        set
        {
            _contadorRegistros = value;
            OnPropertyChanged(nameof(ContadorRegistros));
        }
    }
}
