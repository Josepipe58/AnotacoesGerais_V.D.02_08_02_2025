using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.ConsumoDeGas;

public partial class ConsumoGasViewModel// ConsumoGasComandos
{
    private ICommand _comandoAdicionarConsumoGas;
    public ICommand ComandoAdicionarConsumoGas
    {
        get
        {
            _comandoAdicionarConsumoGas ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de ConsumoGasModel para evitar modificar a instância vinculada à interface do usuário.
                ConsumoGasModel consumoGasModel = new();
                consumoGasModel = ConsumoGasModel;

                if (consumoGasModel.Id == 0 && consumoGasModel.DiasConsumo > 0 && !string.IsNullOrWhiteSpace(consumoGasModel.Preco)
                   && !string.IsNullOrWhiteSpace(consumoGasModel.Fornecedor))
                {
                    try
                    {
                        ConsumoGasRepositorio consumoGasRepositorio = new();
                        ConsumoGas consumoGas = new()
                        {
                            DataCompra = consumoGasModel.DataCompra,
                            DataTroca = consumoGasModel.DataTroca,
                            DiasConsumo = consumoGasModel.DiasConsumo,
                            Preco = Convert.ToDecimal(consumoGasModel.Preco.ToString().Replace("R$", "")),
                            Fornecedor = consumoGasModel.Fornecedor
                        };
                        consumoGasRepositorio.Adicionar(consumoGas);
                        Mensagens.SucessoAoAdicionar(consumoGas.Id);
                        LimparDados();
                    }
                    catch (Exception ex)
                    {
                        Mensagens.NomeDoMetodo = "ComandoAdicionarConsumoGas";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                    }
                }
                else if (consumoGasModel.Id > 0 && consumoGasModel.DiasConsumo > 0 && !string.IsNullOrWhiteSpace(consumoGasModel.Preco)
                        && !string.IsNullOrWhiteSpace(consumoGasModel.Fornecedor))
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
            return _comandoAdicionarConsumoGas;
        }
    }

    private ICommand _comandoEditarConsumoGas;
    public ICommand ComandoEditarConsumoGas
    {
        get
        {
            _comandoEditarConsumoGas ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de ConsumoGasModel para evitar modificar a instância vinculada à interface do usuário.
                ConsumoGasModel consumoGasModel = new();
                consumoGasModel = ConsumoGasModel;

                if (consumoGasModel.Id > 0 && consumoGasModel.DiasConsumo > 0 && !string.IsNullOrWhiteSpace(consumoGasModel.Preco)
                   && !string.IsNullOrWhiteSpace(consumoGasModel.Fornecedor))
                {
                    try
                    {
                        ConsumoGasRepositorio consumoGasRepositorio = new();
                        ConsumoGas consumoGas = new()
                        {
                            Id = consumoGasModel.Id,
                            DataCompra = consumoGasModel.DataCompra,
                            DataTroca = consumoGasModel.DataTroca,
                            DiasConsumo = consumoGasModel.DiasConsumo,
                            Preco = Convert.ToDecimal(consumoGasModel.Preco.ToString().Replace("R$", "")),
                            Fornecedor = consumoGasModel.Fornecedor
                        };
                        consumoGasRepositorio.Editar(consumoGas);
                        Mensagens.SucessoAoEditar(consumoGas.Id);
                        LimparDados();
                    }
                    catch (Exception ex)
                    {
                        Mensagens.NomeDoMetodo = "ComandoEditarConsumoGas";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                    }
                }
                else if (consumoGasModel.Id == 0 && consumoGasModel.DiasConsumo > 0 && !string.IsNullOrWhiteSpace(consumoGasModel.Preco)
                         && !string.IsNullOrWhiteSpace(consumoGasModel.Fornecedor))
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
            return _comandoEditarConsumoGas;
        }
    }

    private ICommand _comandoExcluirConsumoGas;
    public ICommand ComandoExcluirConsumoGas
    {
        get
        {
            _comandoExcluirConsumoGas ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de ConsumoGasModel para evitar modificar a instância vinculada à interface do usuário.
                ConsumoGasModel consumoGasModel = new();
                consumoGasModel = ConsumoGasModel;

                if (consumoGasModel.Id > 0 && !string.IsNullOrWhiteSpace(consumoGasModel.Fornecedor))
                {
                    MessageBoxResult resultado = Mensagens.ConfirmarExcluir(consumoGasModel.Id);
                    if (resultado == MessageBoxResult.No)
                    {
                        LimparDados();
                        return;
                    }
                    try
                    {
                        ConsumoGasRepositorio consumoGasRepositorio = new();
                        ConsumoGas consumoGas = new()
                        {
                            Id = consumoGasModel.Id
                        };
                        consumoGasRepositorio.Excluir(consumoGas);
                        Mensagens.SucessoAoExcluir(consumoGas.Id);
                        LimparDados();
                    }
                    catch (Exception erro)
                    {
                        Mensagens.NomeDoMetodo = "ComandoExcluirConsumoGas";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(erro, Mensagens.NomeDoMetodo);
                        return;
                    }
                }
                else if (consumoGasModel.Id == 0 && !string.IsNullOrWhiteSpace(consumoGasModel.Fornecedor))
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
            return _comandoExcluirConsumoGas;
        }
    }

    private ICommand _comandoAtualizarConsumoGas;
    public ICommand ComandoAtualizarConsumoGas
    {
        get
        {
            _comandoAtualizarConsumoGas ??= new RelayCommand<object>(param =>
            {
                LimparDados();
                ConsumoGasModel.DataAnterior = ListaDaDataAnterior[0].DataTroca;
            });
            return _comandoAtualizarConsumoGas;
        }
    }

    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickConsumoGas;
    public ICommand ComandoDuploClickConsumoGas
    {
        get
        {
            _comandoDuploClickConsumoGas ??= new RelayCommand<object>(param =>
            {
                if (param is ConsumoGas consumoGas)
                {
                    ConsumoGasModel.Id = consumoGas.Id;
                    ConsumoGasModel.DataAnterior = consumoGas.DataTroca;
                    ConsumoGasModel.DataTroca = consumoGas.DataTroca;
                    ConsumoGasModel.DiasConsumo = consumoGas.DiasConsumo;
                    ConsumoGasModel.DataCompra = consumoGas.DataCompra;
                    ConsumoGasModel.Preco = consumoGas.Preco.ToString("C");
                    ConsumoGasModel.Fornecedor = consumoGas.Fornecedor;
                }
            });
            return _comandoDuploClickConsumoGas;
        }
    }

    //Comando do evento KeyDown ao presssionar a tecla Enter.
    private ICommand _comandoCalcularDiasConsumo;
    public ICommand ComandoCalcularDiasConsumo
    {
        get
        {
            _comandoCalcularDiasConsumo ??= new RelayCommand<object>(param =>
            {
                if (param is KeyEventArgs e && e.Key == Key.Enter)
                {
                    if (ConsumoGasModel.IdDataAnterior > 0)
                    {
                        ConsumoGasModel.IndiceSelecionadoConsumoGas = ConsumoGasModel.IdDataAnterior - 1;
                    }
                    TimeSpan diasConsumo;
                    diasConsumo = ConsumoGasModel.DataTroca - ConsumoGasModel.DataAnterior;
                    ConsumoGasModel.DiasConsumo = diasConsumo.Days;

                    //Exibir o resultado do cálculo de dias de consumo em um Label.
                    ConsumoGasModel.CalcularDiasConsumo = $"O consumo de gás, entre {ConsumoGasModel.DataAnterior.ToShortDateString()} e " +
                    $"{ConsumoGasModel.DataTroca.ToShortDateString()} foi de {diasConsumo.Days} dias.";
                }
            });
            return _comandoCalcularDiasConsumo;
        }
    }

    //Comando do evento KeyDown ao presssionar a tecla Enter.
    private ICommand _comandoPrecoGas;
    public ICommand ComandoPrecoGas
    {
        get
        {
            _comandoPrecoGas ??= new RelayCommand<object>(param =>
            {
                try
                {
                    if (param is KeyEventArgs e && e.Key == Key.Enter)
                    {
                        // Obtém o TextBox do sender
                        if (e.OriginalSource is TextBox txt)
                        {
                            if (double.TryParse(txt.Text.ToString().Replace("R$", "").Trim(), out double valorDouble))
                            {
                                txt.Text = string.Format("{0:c}", valorDouble);
                                // Atualiza o valor no ViewModel
                                ConsumoGasModel.Preco = txt.Text;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Mensagens.NomeDoMetodo = "ComandoPrecoGas";
                    Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                    return;
                }
            });
            return _comandoPrecoGas;
        }
    }
}
