using System.Windows;
using System.Windows.Controls;

namespace AppAnotacoesGerais.ExibirDados.Views.Menus;

public partial class CaixaDeSenha : UserControl
{
    // Usando uma Propriedade de Dependência como o armazenamento de apoio para Minha Propriedade.
    // Isso permite animação, estilo, vinculação, etc...
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(CaixaDeSenha), new PropertyMetadata(string.Empty));

    public string Password
    {
        get { return (string)GetValue(PasswordProperty); }
        set { SetValue(PasswordProperty, value); }
    }
    public CaixaDeSenha()
    {
        InitializeComponent();
    }

    private void CaixaDeSenha_PasswordChanged(object sender, RoutedEventArgs e)
    {
        Password = CxaDeSenha.Password;
    }
}
