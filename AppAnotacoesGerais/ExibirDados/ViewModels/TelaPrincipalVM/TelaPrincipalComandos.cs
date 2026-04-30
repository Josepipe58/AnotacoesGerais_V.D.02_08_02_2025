using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Helpers;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.ExibirDados.Views;
using AppAnotacoesGerais.ExibirDados.Views.AnotacoesGeraisView;
using AppAnotacoesGerais.ExibirDados.Views.InformacoesPessoaisView;
using AppAnotacoesGerais.ExibirDados.Views.Menus;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.TelaPrincipalVM;

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

    public void AdicionarAnotacaoGeralComando()
    {
        SelecionarControleDeUsuario = new AdicionarAnotacaoGeralView();
    }

    private ICommand _comandoAdicionarAnotacaoGeral;
    public ICommand ComandoAdicionarAnotacaoGeral
    {
        get
        {
            if (_comandoAdicionarAnotacaoGeral == null)
            {
                _comandoAdicionarAnotacaoGeral = new RelayCommand<object>(param => AdicionarAnotacaoGeralComando());
            }
            return _comandoAdicionarAnotacaoGeral;
        }
    }

    private int _indiceSelecionadoCategoria;
    public int IndiceSelecionadoCategoria
    {
        get => _indiceSelecionadoCategoria;
        set
        {
            if (_indiceSelecionadoCategoria != value)
            {
                _indiceSelecionadoCategoria = value;
                OnPropertyChanged(nameof(IndiceSelecionadoCategoria));
            }
        }
    }

    private ObservableCollection<NomeDescricao> _listaDoNomeDescricao;
    public ObservableCollection<NomeDescricao> ListaDoNomeDescricao
    {
        get => _listaDoNomeDescricao;
        set
        {
            if (_listaDoNomeDescricao != value)
            {
                _listaDoNomeDescricao = value;
                OnPropertyChanged(nameof(ListaDoNomeDescricao));
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private Categoria _categoriaSelecionada;
    public Categoria CategoriaSelecionada
    {
        get => _categoriaSelecionada;
        set
        {
            if (_categoriaSelecionada != value)
            {
                _categoriaSelecionada = value;
                OnPropertyChanged(nameof(CategoriaSelecionada));
                ObterListaDeSubcategorias();
                IndiceSelecionadoCategoria = 0; // Forçar atualização da lista de Subcategoria ao selecionar Categoria
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private Subcategoria _subcategoriaSelecionada = new();
    public Subcategoria SubcategoriaSelecionada
    {
        get => _subcategoriaSelecionada;
        set
        {
            if (_subcategoriaSelecionada != value)
            {
                _subcategoriaSelecionada = value;
                OnPropertyChanged(nameof(SubcategoriaSelecionada));
                ObterListaDeNomeDescricao();
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Subcategorias e o ComboBox de NomeDescricao.
    private NomeDescricao _nomeDescricaoSelecionada = new();
    public NomeDescricao NomeDescricaoSelecionada
    {
        get => _nomeDescricaoSelecionada;
        set
        {
            if (_nomeDescricaoSelecionada != value)
            {
                _nomeDescricaoSelecionada = value;
                OnPropertyChanged(nameof(NomeDescricaoSelecionada));
            }
        }
    }

    //Método do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private void ObterListaDeSubcategorias()
    {
        if (CategoriaSelecionada != null)
        {
            SubcategoriaModel.ListaDeSubcategorias = [.. SubcategoriaRepositorio.ObterSubcategoriasPorId(CategoriaSelecionada.Id) ?? []];
        }
        else
        {
            SubcategoriaModel.ListaDeSubcategorias = [];
        }
    }

    //Método do evento: "SelectionChanged" entre o ComboBox de Sucategorias e o ComboBox de NomeDescricao.
    private void ObterListaDeNomeDescricao()
    {
        if (SubcategoriaSelecionada != null)
        {
            ListaDoNomeDescricao = [.. NomeDescricaoRepositorio.ObterNomeDescricaoPorId(SubcategoriaSelecionada.Id) ?? []];
        }
        else
        {
            ListaDoNomeDescricao = [];
        }
    }

    public void EditarAnotacaoGeralComando()
    {
        try
        {
            AnotacaoGeral anotacaoGeral = new();
            bool retorno = AnotacaoGeralRepositorio.VerificarRegistros(AnotacaoGeralModel.Id);
            if (retorno)
            {
                anotacaoGeral.Id = AnotacaoGeralModel.Id;
                var listaAnotacaoGeral = AnotacaoGeralRepositorio.ObterAnotacoesGeraisPorId(anotacaoGeral.Id);
                if (anotacaoGeral.Id > 0)
                {
                    if (listaAnotacaoGeral.Count >= 0)
                    {
                        if (listaAnotacaoGeral[0].GetType() == typeof(AnotacaoGeral))
                        {
                            anotacaoGeral = listaAnotacaoGeral[0];
                            AnotacaoGeralModel.Id = anotacaoGeral.Id;
                            AnotacaoGeralModel.NomeCategoria = anotacaoGeral.NomeCategoria;
                            AnotacaoGeralModel.NomeSubcategoria = anotacaoGeral.NomeSubcategoria;
                            AnotacaoGeralModel.NomeDaDescricao = anotacaoGeral.NomeDaDescricao;
                            AnotacaoGeralModel.Descricao = anotacaoGeral.Descricao;
                            AnotacaoGeralModel.Data = anotacaoGeral.Data;
                        }
                    }
                    //SelecionarControleDeUsuario = new EditarAnotacaoGeralView(AnotacaoGeralModel);
                }
            }
            else
            {
                Mensagens.NomeDoMetodo = "VerificarRegistros";
                Mensagens.ErroObterId(AnotacaoGeralModel.Id, Mensagens.NomeDoMetodo);
            }
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "EditarAnotacaoGeralComando";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return;
        }
    }

    private ICommand _comandoEditarAnotacaoGeral;
    public ICommand ComandoEditarAnotacaoGeral
    {
        get
        {
            if (_comandoEditarAnotacaoGeral == null)
            {
                _comandoEditarAnotacaoGeral = new RelayCommand<object>(param => EditarAnotacaoGeralComando());
                /*
                {
                    if (param is AnotacaoGeral anotacaoGeral)
                    {
                        // Preencher um modelo que será usado como DataContext na view de edição.
                        var modeloParaEditar = new AnotacaoGeralModel
                        {
                            Id = anotacaoGeral.Id,
                            NomeCategoria = anotacaoGeral.NomeCategoria,
                            NomeSubcategoria = anotacaoGeral.NomeSubcategoria,
                            NomeDaDescricao = anotacaoGeral.NomeDaDescricao,
                            Descricao = anotacaoGeral.Descricao,
                            Data = Convert.ToDateTime(anotacaoGeral.Data.ToString("dd/MM/yyyy"))
                        };

                        // Verificar se o registro existe antes de abrir a view de edição.
                        bool existe = AnotacaoGeralRepositorio.VerificarRegistros(17);//(modeloParaEditar.Id);
                        if (!existe)
                        {
                            Mensagens.NomeDoMetodo = "VerificarRegistros";
                            Mensagens.ErroObterId(modeloParaEditar.Id, Mensagens.NomeDoMetodo);
                            return;
                        }

                        // Abrir a view de edição em um UserControl, passando o modelo clonado e o ViewModel
                        SelecionarControleDeUsuario = new EditarAnotacaoGeralView(modeloParaEditar, null);
                        
                        bool? resultado = userControl?.ShowDialog();
                        if (resultado == true)
                        {
                            // A janela já realizou a persistência; atualize a lista exibida chamando consulta.
                            //AnotacaoGeralViewModel.ConsultasDeAnotacoesGerais();
                        } 
                    }
                });*/
            }
            return _comandoEditarAnotacaoGeral;
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
