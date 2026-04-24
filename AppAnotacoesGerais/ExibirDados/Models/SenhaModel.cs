using AppAnotacoesGerais.ExibirDados.Comandos;

namespace AppAnotacoesGerais.ExibirDados.Models;

public class SenhaModel : ViewModelBase
{
    private string _senha;
    public string Senha
    {
        get => _senha;
        set
        {
            _senha = value;
            OnPropertyChanged(nameof(Senha));
        }
    }
}
