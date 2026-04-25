using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Views.AnotacaoGeral;
using AppAnotacoesGerais.ExibirDados.Views.Menus;
using AppAnotacoesGerais.GerenciarDados;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.TelaPrincipal;

public class MenuModel { public string NomeDoMenu { get; set; } }

public partial class TelaPrincipalViewModel : ViewModelBase
{
    private CollectionViewSource CollectionViewSource { get; set; }
    public ICollectionView SourceCollection => CollectionViewSource.View;

    public int _dataAtual;
    public int DataAtual
    {
        get
        {
            return _dataAtual;
        }
        set
        {
            _dataAtual = value;
            OnPropertyChanged(nameof(DataAtual));
        }
    }

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

    private object _selecionarControleDeUsuario;
    public object SelecionarControleDeUsuario
    {
        get => _selecionarControleDeUsuario;
        set
        {
            _selecionarControleDeUsuario = value;
            OnPropertyChanged(nameof(SelecionarControleDeUsuario));
        }
    }

    public TelaPrincipalViewModel()
    {
        ObservableCollection<MenuModel> menuItems = new ObservableCollection<MenuModel>
        {
            new MenuModel { NomeDoMenu = "Página Inicial" },
            new MenuModel { NomeDoMenu = "Anotações Gerais" },
            new MenuModel { NomeDoMenu = "Informações Pessoais" },
            new MenuModel { NomeDoMenu = "Submenu de Anotações Gerais" },
            new MenuModel { NomeDoMenu = "Banco de Dados" },
            new MenuModel { NomeDoMenu = "Sair do Aplicativo" },
        };

        CollectionViewSource = new CollectionViewSource { Source = menuItems };

        // Configura a página de inicialização.
        SelecionarControleDeUsuario = new PaginaInicial();
        DataAtual = DateTime.Now.Year;
    }

    public void MenuLateral(object parameter)
    {
        try
        {
            var opcao = parameter as string ?? string.Empty;

            switch (opcao)
            {
                case "Página Inicial":
                    SelecionarControleDeUsuario = new PaginaInicial();
                    break;
                case "Anotações Gerais":
                    SelecionarControleDeUsuario = new AnotacaoGeralView();
                    break;
                case "Informações Pessoais":
                    SelecionarControleDeUsuario = new TelaSenha();
                    break;
                case "Submenu de Anotações Gerais":
                    SelecionarControleDeUsuario = new SubmenuAnotacoesGerais();
                    break;
                case "Banco de Dados":
                    try
                    {
                        Process.Start("C:\\Program Files (x86)\\Microsoft SQL Server Management Studio 20\\Common7\\IDE\\Ssms.exe");
                    }
                    catch (Exception ex)
                    {
                        Mensagens.NomeDoMetodo = "MenuLateral";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                        return;
                    }
                    break;
                case "Sair do Aplicativo":
                    Application.Current.Shutdown();
                    break;
                default:
                    SelecionarControleDeUsuario = new PaginaInicial();
                    break;
            }
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "MenuLateral";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return;
        }
    }
}

