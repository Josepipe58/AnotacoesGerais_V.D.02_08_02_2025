using AppAnotacoesGerais.ExibirDados.Views.Menus;
using System.Windows;

namespace AppAnotacoesGerais;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new TelaPrincipalView();
        MainWindow.Show();

        base.OnStartup(e);
    }
}
