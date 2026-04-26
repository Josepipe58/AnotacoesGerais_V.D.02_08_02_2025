using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Helpers;
using AppAnotacoesGerais.ExibirDados.Views;
using AppAnotacoesGerais.ExibirDados.Views.AnotacaoGeral;
using AppAnotacoesGerais.ExibirDados.Views.InformacaoPessoal;
using AppAnotacoesGerais.ExibirDados.Views.Menus;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.TelaPrincipal;

public partial class TelaPrincipalViewModel// TelaPrincipalComandos
{
    #region | Comando do Menu Lateral e Página Inicial |

    private ICommand _comandoMenuLateral;
    public ICommand ComandoMenuLateral
    {
        get
        {
            _comandoMenuLateral ??= new RelayCommand<object>(param => MenuLateral(param));
            return _comandoMenuLateral;
        }
    }

    public void PaginaInicialComando()
    {
        SelecionarControleDeUsuario = new PaginaInicial();
    }

    private ICommand _comandoPaginaInicial;
    public ICommand ComandoPaginaInicial
    {
        get
        {
            if (_comandoPaginaInicial == null)
            {
                _comandoPaginaInicial = new RelayCommand<object>(param => PaginaInicialComando());
            }
            return _comandoPaginaInicial;
        }
    }
    #endregion

    #region | Comandos de Anotaçoes Gerais |

    public void AbrirJanelaDeCadastrar()
    {
        //SelecionarControleDeUsuario = new CadastrarAnotacaoGeral_UI();
    }

    private ICommand _comandoAbrirJanelaDeCadastrar;
    public ICommand ComandoAbrirJanelaDeCadastrar
    {
        get
        {
            if (_comandoAbrirJanelaDeCadastrar == null)
            {
                _comandoAbrirJanelaDeCadastrar = new RelayCommand<object>(param => AbrirJanelaDeCadastrar());
            }
            return _comandoAbrirJanelaDeCadastrar;
        }
    }

    public void VoltarAnotacaoGeral()
    {
        SelecionarControleDeUsuario = new AnotacaoGeralView();
    }

    private ICommand _comandoVoltarAnotacaoGeral;
    public ICommand ComandoVoltarAnotacaoGeral
    {
        get
        {
            if (_comandoVoltarAnotacaoGeral == null)
            {
                _comandoVoltarAnotacaoGeral = new RelayCommand<object>(param => VoltarAnotacaoGeral());
            }
            return _comandoVoltarAnotacaoGeral;
        }
    }

    #endregion

    #region | Senha Para Acessar Informações Pessoais |

    private void VerificarSenha()
    {
        if (Senha == "bj250281")
        {
            SelecionarControleDeUsuario = new InformacaoPessoalView();
            Senha = null;
        }
        else if (string.IsNullOrWhiteSpace(Senha))
        {
            MessageBox.Show($"Digite sua senha para logar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        else if (Senha != "bj250281")
        {
            MessageBox.Show($"Senha inválida, tente novamente.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            SelecionarControleDeUsuario = new TelaSenha();
            return;
        }
    }

    private ICommand _comandoVerificarSenha;
    public ICommand ComandoVerificarSenha
    {
        get
        {
            if (_comandoVerificarSenha == null)
            {
                _comandoVerificarSenha = new RelayCommand<object>(param => VerificarSenha());
            }
            return _comandoVerificarSenha;
        }
    }

    private ICommand _comandoExecutarSenha;
    public ICommand ComandoExecutarSenha
    {
        get
        {
            _comandoExecutarSenha ??= new RelayCommand<object>(param =>
        {
            if (InputHelpers.IsEnterKey(param))
            {
                VerificarSenha();
            }
        });
            return _comandoExecutarSenha;
        }
    }
   
    #endregion   

    #region | Comandos de Categorias, Subcategorias, Nome da Descrição, ConsumoGas e Seta de Voltar|

    public void CategoriaComando()
    {
        SelecionarControleDeUsuario = new CategoriaView();
    }

    private ICommand _comandoCategoria;
    public ICommand ComandoCategoria
    {
        get
        {
            if (_comandoCategoria == null)
            {
                _comandoCategoria = new RelayCommand<object>(param => CategoriaComando());
            }
            return _comandoCategoria;
        }
    }

    public void SubcategoriaComando()
    {
        SelecionarControleDeUsuario = new SubcategoriaView();        
    }

    private ICommand _comandoSubcategoria;
    public ICommand ComandoSubcategoria
    {
        get
        {
            if (_comandoSubcategoria == null)
            {
                _comandoSubcategoria = new RelayCommand<object>(param => SubcategoriaComando());
            }
            return _comandoSubcategoria;
        }
    }

    public void NomeDescricaoComando()
    {
        SelecionarControleDeUsuario = new NomeDescricaoView();
    }

    private ICommand _comandoNomeDescricao;
    public ICommand ComandoNomeDescricao
    {
        get
        {
            if (_comandoNomeDescricao == null)
            {
                _comandoNomeDescricao = new RelayCommand<object>(param => NomeDescricaoComando());
            }
            return _comandoNomeDescricao;
        }
    }

    public void ConsumoGasComando()
    {
        SelecionarControleDeUsuario = new ConsumoGasView();
    }

    private ICommand _comandoConsumoGas;
    public ICommand ComandoConsumoGas
    {
        get
        {
            if (_comandoConsumoGas == null)
            {
                _comandoConsumoGas = new RelayCommand<object>(param => ConsumoGasComando());
            }
            return _comandoConsumoGas;
        }
    }

    private void VoltarSubmenuAnotacoesGerais()
    {
        SelecionarControleDeUsuario = new SubmenuAnotacoesGerais();
    }

    private ICommand _comandoVoltarSubmenuAnotacoesGerais;
    public ICommand ComandoVoltarSubmenuAnotacoesGerais
    {
        get
        {
            if (_comandoVoltarSubmenuAnotacoesGerais == null)
            {
                _comandoVoltarSubmenuAnotacoesGerais =
                    new RelayCommand<object>(param => VoltarSubmenuAnotacoesGerais());
            }
            return _comandoVoltarSubmenuAnotacoesGerais;
        }
    }
    #endregion

    #region | Banco de Dados e Sair do Aplicativo |
    public static void BancoDadosComando()
    {
        Process.Start("C:\\Program Files (x86)\\Microsoft SQL Server Management Studio 20\\Common7\\IDE\\Ssms.exe");
    }

    private ICommand _comandoBancoDados;
    public ICommand ComandoBancoDados
    {
        get
        {
            if (_comandoBancoDados == null)
            {
                _comandoBancoDados = new RelayCommand<object>(param => BancoDadosComando());
            }
            return _comandoBancoDados;
        }
    }

    public static void SairAplicativoComando()
    {
        Application.Current.Shutdown();
    }

    private ICommand _comandoSairAplicativo;
    public ICommand ComandoSairAplicativo
    {
        get
        {
            if (_comandoSairAplicativo == null)
            {
                _comandoSairAplicativo = new RelayCommand<object>(param => SairAplicativoComando());
            }
            return _comandoSairAplicativo;
        }
    }
    #endregion

    // Uso de método utilitário compartilhado para verificação de tecla Enter.
    /*
    public void FecharAplicativoComando()
    {

        var telaLogin = Application.Current.Windows.OfType<Window>()
            .FirstOrDefault(w => w.GetType() == typeof(TelaSenha));
        telaLogin.Close();

        var telaPrincipal = Application.Current.Windows.OfType<Window>().First();
        telaPrincipal.Close();

        //TelaPrincipal.Show();

        Window ReabrirTelaPrincipal = new Views.Menus.TelaPrincipal();
        ReabrirTelaPrincipal.Show();
    }

    private ICommand _comandoFecharAplicativo;
    public ICommand ComandoFecharAplicativo
    {
        get
        {
            if (_comandoFecharAplicativo == null)
            {
                _comandoFecharAplicativo = new RelayCommand<object>(param => FecharAplicativoComando());
            }
            return _comandoFecharAplicativo;
        }
    }
    */
}
