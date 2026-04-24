using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Helpers;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.ExibirDados.Views;
using AppAnotacoesGerais.ExibirDados.Views.Menus;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.TelaPrincipal;

public partial class TelaPrincipalViewModel// TelaPrincipalComandos
{
    //public ICommand ComandoMenuLateral { get; }


    public void PaginaInicialComando()
    {
        SelecionarControleDeUsuario = new PaginaInicial();
        Caption = "Página Inicial";       
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

    // Uso de método utilitário compartilhado para verificação de tecla Enter.

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

    #endregion

    #region | Comandos de AnotacoesGerais, InformacoesPessoais e ConsumoGas |

    public void InformacaoPessoalComando()
    {
        SelecionarControleDeUsuario = new TelaSenha();
    }

    private ICommand _comandoInformacaoPessoal;
    public ICommand ComandoInformacaoPessoal
    {
        get
        {
            if (_comandoInformacaoPessoal == null)
            {
                _comandoInformacaoPessoal = new RelayCommand<object>(param => InformacaoPessoalComando());
            }
            return _comandoInformacaoPessoal;
        }
    }
    #endregion

    #region | Comandos de Categorias, Subcategorias e Nome da Descrição |

    public void CategoriaComando()
    {
        SelecionarControleDeUsuario = new CategoriaView();
        Caption = "Gerenciar Categorias";        
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
        Caption = "Gerenciar Subcategorias";        
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

    public void NomeDaDescricaoComando()
    {
        SelecionarControleDeUsuario = new NomeDescricaoView();
        Caption = "Gerenciar Nome da Descrição";        
    }

    private ICommand _comandoNomeDaDescricao;
    public ICommand ComandoNomeDaDescricao
    {
        get
        {
            if (_comandoNomeDaDescricao == null)
            {
                _comandoNomeDaDescricao = new RelayCommand<object>(param => NomeDaDescricaoComando());
            }
            return _comandoNomeDaDescricao;
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
}
