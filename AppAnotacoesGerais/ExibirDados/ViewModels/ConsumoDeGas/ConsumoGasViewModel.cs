using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.ConsumoDeGas;

public partial class ConsumoGasViewModel : ViewModelBase
{
    public ConsumoGasRepositorio _consumoGasRepositorio = new();

    public ConsumoGasModel ConsumoGasModel { get; set; } = new();

    private List<ConsumoGas> _listaDaDataAnterior;
    public List<ConsumoGas> ListaDaDataAnterior
    {
        get => _listaDaDataAnterior;
        set
        {
            if (_listaDaDataAnterior != value)
            {
                _listaDaDataAnterior = value;
                OnPropertyChanged(nameof(ListaDaDataAnterior));
            }
        }
    }

    private ObservableCollection<ConsumoGas> _listaDeConsumoGas;
    public ObservableCollection<ConsumoGas> ListaDeConsumoGas
    {
        get => _listaDeConsumoGas;
        set
        {
            if (_listaDeConsumoGas != value)
            {
                _listaDeConsumoGas = value;
                OnPropertyChanged(nameof(ListaDeConsumoGas));
            }
        }
    }

    public ConsumoGasViewModel()
    {
        ConsumoGasModel.CalcularDiasConsumo = $"Quantidade de dias de consumo.";

        _listaDaDataAnterior = [.. _consumoGasRepositorio.ObterListaDeTodos().OrderByDescending(x => x.DataTroca)];
        ListaDaDataAnterior = [.. _listaDaDataAnterior ?? []];

        _listaDeConsumoGas = [.. _consumoGasRepositorio.ObterListaDeTodos() ?? []];

        ListaDeConsumoGas = new ObservableCollection<ConsumoGas>(_listaDeConsumoGas.OrderByDescending(x => x.Id));

        VerificarGasDeReserva();
    }

    private void VerificarGasDeReserva()
    {
        try
        {
            var consumo = _consumoGasRepositorio.ObterListaDeTodos().OrderByDescending(x => x.Id).ToList();
            if (consumo[0].Preco > 0)
            {
                ConsumoGasModel.BotijaoReserva = "Sim";
            }
            else
            {
                ConsumoGasModel.BotijaoReserva = "Não";
            }
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ComandoVerificarGasDeReserva";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return;
        }
    }
}
