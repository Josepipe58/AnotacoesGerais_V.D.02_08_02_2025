using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.ExibirDados.ViewModels.TelaPrincipalVM;
using AppAnotacoesGerais.ExibirDados.Views.AnotacoesGeraisView;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.AnotacoesGerais;

public partial class AnotacaoGeralViewModel// AnotacaoGeralComandos
{
    /*
    private ICommand _comandoAdicionarAnotacaoGeral;
    public ICommand ComandoAdicionarAnotacaoGeral
    {
        get
        {
            _comandoAdicionarAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de AnotacaoGeralModel para evitar
                // modificar a instância vinculada à interface do usuário.
                AnotacaoGeralModel anotacaoGeralModel = new();
                anotacaoGeralModel = AnotacaoGeralModel;

                if (anotacaoGeralModel.Id == 0 && !string.IsNullOrWhiteSpace(anotacaoGeralModel.Descricao))
                {
                    try
                    {
                        CategoriaRepositorio categoriaRepositorio = new();
                        AnotacaoGeral anotacaoGeral = new()
                        {
                            NomeCategoria = anotacaoGeralModel.NomeCategoria,
                            NomeSubcategoria = anotacaoGeralModel.NomeSubcategoria,
                            NomeDaDescricao = anotacaoGeralModel.NomeDaDescricao,
                            Descricao = anotacaoGeralModel.Descricao,
                            Data = anotacaoGeralModel.Data,
                        };
                        _anotacaoGeralRepositorio.Adicionar(anotacaoGeral);
                        Mensagens.SucessoAoAdicionar(anotacaoGeral.Id);
                        LimparDados();
                    }
                    catch (Exception ex)
                    {
                        Mensagens.NomeDoMetodo = "ComandoAdicionarAnotacaoGeral";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                    }
                }
                else if (anotacaoGeralModel.Id > 0 && !string.IsNullOrWhiteSpace(anotacaoGeralModel.Descricao))
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
            return _comandoAdicionarAnotacaoGeral;
        }
    }

    private ICommand _comandoEditarAnotacaoGeral;
    public ICommand ComandoEditarAnotacaoGeral
    {
        get
        {
            _comandoEditarAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de AnotacaoGeralModel para evitar
                // modificar a instância vinculada à interface do usuário.
                AnotacaoGeralModel anotacaoGeralModel = new();
                anotacaoGeralModel = AnotacaoGeralModel;

                if (anotacaoGeralModel.Id > 0 && !string.IsNullOrWhiteSpace(anotacaoGeralModel.Descricao))
                {
                    try
                    {
                        CategoriaRepositorio categoriaRepositorio = new();
                        AnotacaoGeral anotacaoGeral = new()
                        {
                            Id = anotacaoGeralModel.Id,
                            NomeCategoria = anotacaoGeralModel.NomeCategoria,
                            NomeSubcategoria = anotacaoGeralModel.NomeSubcategoria,
                            NomeDaDescricao = anotacaoGeralModel.NomeDaDescricao,
                            Descricao = anotacaoGeralModel.Descricao,
                            Data = anotacaoGeralModel.Data,
                        };
                        _anotacaoGeralRepositorio.Editar(anotacaoGeral);
                        Mensagens.SucessoAoEditar(anotacaoGeral.Id);
                        LimparDados();
                    }
                    catch (Exception ex)
                    {
                        Mensagens.NomeDoMetodo = "ComandoEditarAnotacaoGeral";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                    }
                }
                else if (anotacaoGeralModel.Id == 0 && !string.IsNullOrWhiteSpace(anotacaoGeralModel.Descricao))
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
            return _comandoEditarAnotacaoGeral;
        }
    }*/

    private ICommand _comandoExcluirAnotacaoGeral;
    public ICommand ComandoExcluirAnotacaoGeral
    {
        get
        {
            _comandoExcluirAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                AnotacaoGeralModel anotacaoGeralModel = new();
                anotacaoGeralModel = AnotacaoGeralModel;

                if (anotacaoGeralModel.Id > 0)
                {
                    MessageBoxResult resultado = Mensagens.ConfirmarExcluir(anotacaoGeralModel.Id);
                    if (resultado == MessageBoxResult.No)
                    {
                        LimparDados();
                        return;
                    }
                    try
                    {
                        AnotacaoGeralRepositorio anotacaoGeralRepositorio = new();
                        AnotacaoGeral anotacaoGeral = new()
                        {
                            Id = anotacaoGeralModel.Id
                        };
                        anotacaoGeralRepositorio.Excluir(anotacaoGeral);
                        Mensagens.SucessoAoExcluir(anotacaoGeral.Id);
                        LimparDados(); 
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
            return _comandoExcluirAnotacaoGeral;
        }
    }

    private ICommand _comandoAtualizarAnotacaoGeral;
    public ICommand ComandoAtualizarAnotacaoGeral
    {
        get
        {
            _comandoAtualizarAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                AnotacaoGeralModel.Id = 0;
                AnotacaoGeralModel.NomeCategoria = "";
                AnotacaoGeralModel.NomeSubcategoria = "";
                AnotacaoGeralModel.NomeDaDescricao = "";
                ConsultasDeAnotacoesGerais();
            });
            return _comandoAtualizarAnotacaoGeral;
        }
    }
    
    private ICommand _comandoGerenciarAnotacaoGeral;
    public ICommand ComandoGerenciarAnotacaoGeral
    {
        get
        {
            _comandoGerenciarAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                try
                {
                    EditarAnotacaoGeralView editarAnotacaoGeralView = new(AnotacaoGeralModel, null);
                    AnotacaoGeral anotacaoGeral = new();
                    ObservableCollection<AnotacaoGeralModel> listaDeAnotacaoGeralModel = new ObservableCollection<AnotacaoGeralModel>();
                    AnotacaoGeralModel.Id = Convert.ToInt32(editarAnotacaoGeralView.TxtId.Text);
                    bool retorno = AnotacaoGeralRepositorio.VerificarRegistros(AnotacaoGeralModel.Id);
                    if (retorno)
                    {
                        AnotacaoGeralModel.Id = AnotacaoGeralModel.Id;
                        var listaAnotacaoGeral = AnotacaoGeralRepositorio.ObterAnotacoesGeraisPorId(AnotacaoGeralModel.Id);

                        foreach (var item in listaAnotacaoGeral)
                            listaDeAnotacaoGeralModel.Add(new AnotacaoGeralModel
                            {
                                Id = item.Id,
                                NomeCategoria = item.NomeCategoria,
                                NomeSubcategoria = item.NomeSubcategoria,
                                NomeDaDescricao = item.NomeDaDescricao,
                                Descricao = item.Descricao,
                                Data = item.Data
                            });

                        if (listaDeAnotacaoGeralModel.Count > 0 && listaDeAnotacaoGeralModel[0].Id > 0)
                        {
                            if (listaDeAnotacaoGeralModel.Count >= 0)
                            {
                                if (listaDeAnotacaoGeralModel[0].GetType() == typeof(AnotacaoGeralModel))
                                {
                                    AnotacaoGeralModel = listaDeAnotacaoGeralModel[0];
                                    editarAnotacaoGeralView.TxtId.Text = Convert.ToString(AnotacaoGeralModel.Id.ToString());
                                    editarAnotacaoGeralView.CbxCategoria.Text = AnotacaoGeralModel.NomeCategoria;
                                    editarAnotacaoGeralView.CbxSubcategoria.Text = AnotacaoGeralModel.NomeSubcategoria;
                                    editarAnotacaoGeralView.CbxNomeDaDescricao.Text = AnotacaoGeralModel.NomeDaDescricao;
                                    editarAnotacaoGeralView.TxtDescricao.Text = AnotacaoGeralModel.Descricao;
                                    editarAnotacaoGeralView.DtpData.Text = AnotacaoGeralModel.Data.ToString();

                                    // Atribui a view de edição ao ViewModel principal para que o ContentControl principal exiba a UserControl
                                    var mainVm = Application.Current?.MainWindow?.DataContext as TelaPrincipalViewModel;
                                    if (mainVm != null)
                                    {
                                        AnotacaoGeralModel.DiferenciarMetodos = "GerenciarAnotacaoGeral";
                                        mainVm.SelecionarControleDeUsuario = new EditarAnotacaoGeralView(AnotacaoGeralModel, null);
                                    }
                                }
                            }
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
            });
            return _comandoGerenciarAnotacaoGeral;
        }
    }
    
    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickAnotacaoGeral;
    public ICommand ComandoDuploClickAnotacaoGeral
    {
        get
        {
            _comandoDuploClickAnotacaoGeral ??= new RelayCommand<object>(param => 
            /*{ 
                // Atribui a view de edição ao ViewModel principal para que o ContentControl principal exiba a UserControl
                var mainVm = Application.Current?.MainWindow?.DataContext as TelaPrincipalViewModel;
                if (mainVm != null)
                {
                    mainVm.EditarAnotacaoGeralExcluirComando();//.SelecionarControleDeUsuario = new EditarAnotacaoGeralView(AnotacaoGeralModel, null);// editarAnotacaoGeralView;
                }
            });*/
            {               
                if (param is AnotacaoGeral anotacaoGeral)
                {
                    AnotacaoGeralModel.Id = anotacaoGeral.Id;
                    /*
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
                    bool existe = AnotacaoGeralRepositorio.VerificarRegistros(modeloParaEditar.Id);
                    if (!existe)
                    {
                        Mensagens.NomeDoMetodo = "VerificarRegistros";
                        Mensagens.ErroObterId(modeloParaEditar.Id, Mensagens.NomeDoMetodo);
                        return;
                    }

                    // Abrir a view de edição em uma janela modal, passando o modelo clonado e o ViewModel atual
                    var window = new EditarAnotacaoGeralWindow(modeloParaEditar, this);
                    bool? resultado = window.ShowDialog();
                    if (resultado == true)
                    {
                        // A janela já realizou a persistência; atualize a lista exibida chamando consulta.
                        ConsultasDeAnotacoesGerais();
                    }*/
                }               
            });
            return _comandoDuploClickAnotacaoGeral;
        }
    }
}
