using System.ComponentModel;

namespace AppAnotacoesGerais.ExibirDados.Comandos;

public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    void IDisposable.Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
