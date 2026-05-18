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

    private ObservableCollection<ConsumoGas> _listaDaDataAnterior;
    public ObservableCollection<ConsumoGas> ListaDaDataAnterior
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

        //Não mudar essa lista senão o combobox ficará vazio ao limpar os dados.
        _listaDaDataAnterior = [.. _consumoGasRepositorio.ObterListaDeTodos() ?? []];
        ListaDaDataAnterior = new ObservableCollection<ConsumoGas>(_listaDaDataAnterior.OrderByDescending(x => x.DataTroca));

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

    public void LimparDados()
    {
        ConsumoGasModel.Id = 0;
        ConsumoGasModel.DiasConsumo = 0;
        ConsumoGasModel.Preco = "";
        ConsumoGasModel.Fornecedor = null;
        ConsumoGasModel.DataCompra = DateTime.Now;
        ConsumoGasModel.DataTroca = DateTime.Now;

        var listaDeConsumoGas = _consumoGasRepositorio.ObterListaDeTodos()
            .OrderByDescending(x => x.DataTroca).ToList() ?? [];

        //Carregar DataGrid de ConsumoGass.        
        ListaDeConsumoGas = new ObservableCollection<ConsumoGas>(listaDeConsumoGas);
    }
}
