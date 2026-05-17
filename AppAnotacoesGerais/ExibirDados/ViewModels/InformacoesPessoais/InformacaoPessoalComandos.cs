using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.ExibirDados.ViewModels.TelaPrincipal;
using AppAnotacoesGerais.ExibirDados.Views.InformacoesPessoaisView;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.InformacoesPessoais;

public partial class InformacaoPessoalViewModel//InformacaoPessoalComandos
{
    private ICommand _comandoAbrirJanelaGerenciarInformacaoPessoal;
    public ICommand ComandoAbrirJanelaGerenciarInformacaoPessoal
    {
        get
        {
            _comandoAbrirJanelaGerenciarInformacaoPessoal ??= new RelayCommand<object>(param =>
            {
                try
                {
                    InformacaoPessoalGerenciarView informacaoPessoalGerenciarView = new(InformacaoPessoalModel, null);
                    InformacaoPessoal informacaoPessoal = new();
                    ObservableCollection<InformacaoPessoalModel> listaDeInformacaoPessoalModel = new ObservableCollection<InformacaoPessoalModel>();
                    bool retorno = InformacaoPessoalRepositorio.VerificarRegistros(InformacaoPessoalModel.Id);
                    if (retorno)
                    {
                        InformacaoPessoalModel.Id = InformacaoPessoalModel.Id;
                        var listaInformacaoPessoal = InformacaoPessoalRepositorio.ObterInformacoesPessoaisPorId(InformacaoPessoalModel.Id);

                        foreach (var item in listaInformacaoPessoal)
                            listaDeInformacaoPessoalModel.Add(new InformacaoPessoalModel
                            {
                                Id = item.Id,
                                Titulo = item.Titulo,
                                Descricao = item.Descricao,
                            });

                        if (listaDeInformacaoPessoalModel.Count > 0 && listaDeInformacaoPessoalModel[0].Id > 0)
                        {
                            if (listaDeInformacaoPessoalModel.Count >= 0)
                            {
                                if (listaDeInformacaoPessoalModel[0].GetType() == typeof(InformacaoPessoalModel))
                                {
                                    InformacaoPessoalModel = listaDeInformacaoPessoalModel[0];
                                    informacaoPessoalGerenciarView.TxtId.Text = Convert.ToString(InformacaoPessoalModel.Id.ToString());
                                    informacaoPessoalGerenciarView.TxtTitulo.Text = InformacaoPessoalModel.Titulo;
                                    informacaoPessoalGerenciarView.TxtDescricao.Text = InformacaoPessoalModel.Descricao;

                                    // Atribui a view de edição ao ViewModel principal para que o ContentControl principal exiba a UserControl
                                    var mainVm = Application.Current?.MainWindow?.DataContext as TelaPrincipalViewModel;
                                    if (mainVm != null)
                                    {
                                        mainVm.SelecionarControleDeUsuario = new InformacaoPessoalGerenciarView(InformacaoPessoalModel, null);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Mensagens.NomeDoMetodo = "VerificarRegistros";
                        Mensagens.ErroObterId(InformacaoPessoalModel.Id, Mensagens.NomeDoMetodo);
                    }
                }
                catch (Exception ex)
                {
                    Mensagens.NomeDoMetodo = "ComandoAbrirJanelaGerenciarInformacaoPessoal";
                    Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                    return;
                }
            });
            return _comandoAbrirJanelaGerenciarInformacaoPessoal;
        }
    }

    private ICommand _comandoVoltarInformacaoPessoal;
    public ICommand ComandoVoltarInformacaoPessoal
    {
        get
        {
            if (_comandoVoltarInformacaoPessoal == null)
            {
                _comandoVoltarInformacaoPessoal = new RelayCommand<object>(param =>
                {
                    var mainVm = Application.Current?.MainWindow?.DataContext as TelaPrincipalViewModel;
                    if (mainVm != null)
                    {
                        mainVm.SelecionarControleDeUsuario = new InformacaoPessoalView();
                    }
                });
            }
            return _comandoVoltarInformacaoPessoal;
        }
    }

    private ICommand _comandoAdicionarInformacaoPessoal;
    public ICommand ComandoAdicionarInformacaoPessoal
    {
        get
        {
            _comandoAdicionarInformacaoPessoal ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de InformacaoPessoalModel para evitar
                // modificar a instância vinculada à interface do usuário.
                InformacaoPessoalModel informacaoPessoalModel = new();
                informacaoPessoalModel = InformacaoPessoalModel;

                if (informacaoPessoalModel.Id == 0 && !string.IsNullOrWhiteSpace(informacaoPessoalModel.Titulo)
                && !string.IsNullOrWhiteSpace(informacaoPessoalModel.Descricao))
                {
                    try
                    {
                        CategoriaRepositorio categoriaRepositorio = new();
                        InformacaoPessoal informacaoPessoal = new()
                        {
                            Titulo = informacaoPessoalModel.Titulo,
                            Descricao = informacaoPessoalModel.Descricao,
                        };
                        _informacaoPessoalRepositorio.Adicionar(informacaoPessoal);
                        Mensagens.SucessoAoAdicionar(informacaoPessoal.Id);

                        LimparAdicionarEditar();
                    }
                    catch (Exception ex)
                    {
                        Mensagens.NomeDoMetodo = "ComandoAdicionarInformacaoPessoal";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                    }
                }
                else if (informacaoPessoalModel.Id > 0 && !string.IsNullOrWhiteSpace(informacaoPessoalModel.Titulo)
                && !string.IsNullOrWhiteSpace(informacaoPessoalModel.Descricao))
                {
                    Mensagens.ErroAoAdicionar();
                    return;
                }
                else
                {
                    Mensagens.PreencherCampoVazio();
                    return;
                }
            });
            return _comandoAdicionarInformacaoPessoal;
        }
    }

    private ICommand _comandoEditarInformacaoPessoal;
    public ICommand ComandoEditarInformacaoPessoal
    {
        get
        {
            _comandoEditarInformacaoPessoal ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de InformacaoPessoalModel para evitar
                // modificar a instância vinculada à interface do usuário.
                InformacaoPessoalModel informacaoPessoalModel = new();
                informacaoPessoalModel = InformacaoPessoalModel;

                if (informacaoPessoalModel.Id > 0 && !string.IsNullOrWhiteSpace(informacaoPessoalModel.Titulo)
                && !string.IsNullOrWhiteSpace(informacaoPessoalModel.Descricao))
                {
                    try
                    {
                        CategoriaRepositorio categoriaRepositorio = new();
                        InformacaoPessoal informacaoPessoal = new()
                        {
                            Id = informacaoPessoalModel.Id,
                            Titulo = informacaoPessoalModel.Titulo,
                            Descricao = informacaoPessoalModel.Descricao,
                        };
                        _informacaoPessoalRepositorio.Editar(informacaoPessoal);
                        Mensagens.SucessoAoEditar(informacaoPessoal.Id);

                        LimparAdicionarEditar();
                    }
                    catch (Exception ex)
                    {
                        Mensagens.NomeDoMetodo = "ComandoEditarInformacaoPessoal";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                    }
                }
                else if (informacaoPessoalModel.Id == 0 && !string.IsNullOrWhiteSpace(informacaoPessoalModel.Titulo)
                && !string.IsNullOrWhiteSpace(informacaoPessoalModel.Descricao))
                {
                    Mensagens.ErroAoEditarOuExcluir();
                    return;
                }
                else
                {
                    Mensagens.PreencherCampoVazio();
                    return;
                }
            });
            return _comandoEditarInformacaoPessoal;
        }
    }

    private ICommand _comandoExcluirInformacaoPessoal;
    public ICommand ComandoExcluirInformacaoPessoal
    {
        get
        {
            _comandoExcluirInformacaoPessoal ??= new RelayCommand<object>(param =>
            {
                InformacaoPessoalModel informacaoPessoalModel = new();
                informacaoPessoalModel = InformacaoPessoalModel;

                if (informacaoPessoalModel.Id > 0)
                {
                    MessageBoxResult resultado = Mensagens.ConfirmarExcluir(informacaoPessoalModel.Id);
                    if (resultado == MessageBoxResult.No)
                    {
                        AtualizarInformacaoPessoal();
                        return;
                    }
                    try
                    {
                        InformacaoPessoalRepositorio informacaoPessoalRepositorio = new();
                        InformacaoPessoal informacaoPessoal = new()
                        {
                            Id = informacaoPessoalModel.Id
                        };
                        informacaoPessoalRepositorio.Excluir(informacaoPessoal);
                        Mensagens.SucessoAoExcluir(informacaoPessoal.Id);

                        AtualizarInformacaoPessoal();
                        return;
                    }
                    catch (Exception erro)
                    {
                        Mensagens.NomeDoMetodo = "Excluir";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(erro, Mensagens.NomeDoMetodo);
                        return;
                    }
                }
                else
                {
                    Mensagens.PreencherCampoVazio();
                    return;
                }
            });
            return _comandoExcluirInformacaoPessoal;
        }
    }

    private ICommand _comandoAtualizarInformacaoPessoal;
    public ICommand ComandoAtualizarInformacaoPessoal
    {
        get
        {
            _comandoAtualizarInformacaoPessoal ??= new RelayCommand<object>(param => AtualizarInformacaoPessoal());

            return _comandoAtualizarInformacaoPessoal;
        }
    }

    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickInformacaoPessoal;
    public ICommand ComandoDuploClickInformacaoPessoal
    {
        get
        {
            _comandoDuploClickInformacaoPessoal ??= new RelayCommand<object>(param =>
            {
                if (param is InformacaoPessoal informacaoPessoal)
                {
                    InformacaoPessoalModel.Id = informacaoPessoal.Id;
                }
            });
            return _comandoDuploClickInformacaoPessoal;
        }
    }

    private ICommand _comandoAtualizarInformacaoPessoalGerenciarView;
    public ICommand ComandoAtualizarInformacaoPessoalGerenciarView
    {
        get
        {
            _comandoAtualizarInformacaoPessoalGerenciarView ??= new RelayCommand<object>(param => LimparAdicionarEditar());

            return _comandoAtualizarInformacaoPessoalGerenciarView;
        }
    }
}
