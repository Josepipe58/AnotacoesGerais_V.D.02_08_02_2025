using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Views;
using AppAnotacoesGerais.ExibirDados.Views.InformacoesPessoaisView;
using AppAnotacoesGerais.ExibirDados.Views.Menus;
using AppAnotacoesGerais.ExibirDados.Views.TelaSenha;
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

    #region | Senha Para Acessar Informações Pessoais |

    private void VerificarSenha()
    {
        if (Senha == "bj250281" && !string.IsNullOrWhiteSpace(Senha))
        {
            SelecionarControleDeUsuario = new InformacaoPessoalView();
            Senha = null;
        }
        else if (string.IsNullOrWhiteSpace(Senha))
        {
            MessageBox.Show($"Digite sua senha para logar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            SelecionarControleDeUsuario = new TelaSenhaView();
            return;
        }
        else if (Senha != "bj250281")
        {
            MessageBox.Show($"Senha incorreta, tente novamente.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            Senha = null;
            SelecionarControleDeUsuario = new TelaSenhaView();
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
                if (param is KeyEventArgs e && e.Key == Key.Enter)
                {
                    VerificarSenha();
                }
            });
            return _comandoExecutarSenha;
        }
    }
    #endregion

    #region | Comandos de Categorias, Subcategorias, Nome da Descrição, ConsumoGas e Seta de Voltar|

    private ICommand _comandoCategoria;
    public ICommand ComandoCategoria
    {
        get
        {
            if (_comandoCategoria == null)
            {
                _comandoCategoria = new RelayCommand<object>(param =>
                {
                    SelecionarControleDeUsuario = new CategoriaView();
                });
            }
            return _comandoCategoria;
        }
    }

    private ICommand _comandoSubcategoria;
    public ICommand ComandoSubcategoria
    {
        get
        {
            if (_comandoSubcategoria == null)
            {
                _comandoSubcategoria = new RelayCommand<object>(param =>
                {
                    SelecionarControleDeUsuario = new SubcategoriaView();
                });
            }
            return _comandoSubcategoria;
        }
    }

    private ICommand _comandoNomeDescricao;
    public ICommand ComandoNomeDescricao
    {
        get
        {
            if (_comandoNomeDescricao == null)
            {
                _comandoNomeDescricao = new RelayCommand<object>(param =>
                {
                    SelecionarControleDeUsuario = new NomeDescricaoView();
                });
            }
            return _comandoNomeDescricao;
        }
    }

    private ICommand _comandoConsumoGas;
    public ICommand ComandoConsumoGas
    {
        get
        {
            if (_comandoConsumoGas == null)
            {
                _comandoConsumoGas = new RelayCommand<object>(param =>
                {
                    SelecionarControleDeUsuario = new ConsumoGasView();
                });
            }
            return _comandoConsumoGas;
        }
    }

    private ICommand _comandoVoltarSubmenuAnotacoesGerais;
    public ICommand ComandoVoltarSubmenuAnotacoesGerais
    {
        get
        {
            if (_comandoVoltarSubmenuAnotacoesGerais == null)
            {
                _comandoVoltarSubmenuAnotacoesGerais =
                    new RelayCommand<object>(param =>
                    {
                        SelecionarControleDeUsuario = new SubmenuAnotacoesGerais();
                    });
            }
            return _comandoVoltarSubmenuAnotacoesGerais;
        }
    }
    #endregion
    
}
