using AppAnotacoesGerais.ExibirDados.Comandos;

namespace AppAnotacoesGerais.ExibirDados.Models;

public class InformacaoPessoalModel : ViewModelBase
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

    private string _titulo;
    public string Titulo
    {
        get => _titulo;
        set
        {
            _titulo = value;
            OnPropertyChanged(nameof(Titulo));
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
}
