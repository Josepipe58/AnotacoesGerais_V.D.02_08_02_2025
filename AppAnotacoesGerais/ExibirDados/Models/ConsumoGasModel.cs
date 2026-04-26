using AppAnotacoesGerais.ExibirDados.Comandos;

namespace AppAnotacoesGerais.ExibirDados.Models;

public class ConsumoGasModel : ViewModelBase
{
    private int _id;
    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    private int _diasConsumo;
    public int DiasConsumo
    {
        get => _diasConsumo;
        set
        {
            _diasConsumo = value;
            OnPropertyChanged(nameof(DiasConsumo));
        }
    }

    private DateTime _dataTroca = DateTime.Now;
    public DateTime DataTroca
    {
        get => _dataTroca;
        set
        {
            _dataTroca = value;
            OnPropertyChanged(nameof(DataTroca));
        }
    }

    private DateTime _dataCompra = DateTime.Now;
    public DateTime DataCompra
    {
        get => _dataCompra;
        set
        {
            _dataCompra = value;
            OnPropertyChanged(nameof(DataCompra));
        }
    }

    private string _preco;
    public string Preco
    {
        get => _preco;
        set
        {
            _preco = value;
            OnPropertyChanged(nameof(Preco));
        }
    }

    private string _fornecedor;
    public string Fornecedor
    {
        get => _fornecedor;
        set
        {
            _fornecedor = value;
            OnPropertyChanged(nameof(Fornecedor));
        }
    }

    private int _idDataAnterior;
    public int IdDataAnterior
    {
        get => _idDataAnterior;
        set
        {
            _idDataAnterior = value;
            OnPropertyChanged(nameof(IdDataAnterior));
        }
    }

    private string _calcularDiasConsumo;
    public string CalcularDiasConsumo
    {
        get => _calcularDiasConsumo;
        set
        {
            _calcularDiasConsumo = value;
            OnPropertyChanged(nameof(CalcularDiasConsumo));
        }
    }

    private DateTime _dataAnterior;
    public DateTime DataAnterior
    {
        get => _dataAnterior;
        set
        {
            _dataAnterior = value;
            OnPropertyChanged(nameof(DataAnterior));
        }
    }
    //TxtBotijaoReserva
    private string _botijaoReserva;
    public string BotijaoReserva
    {
        get => _botijaoReserva;
        set
        {
            _botijaoReserva = value;
            OnPropertyChanged(nameof(BotijaoReserva));
        }
    }

    private int _indiceSelecionadoConsumoGas;
    public int IndiceSelecionadoConsumoGas
    {
        get => _indiceSelecionadoConsumoGas;
        set
        {
            if (_indiceSelecionadoConsumoGas != value)
            {
                _indiceSelecionadoConsumoGas = value;
                OnPropertyChanged(nameof(IndiceSelecionadoConsumoGas));
            }
        }
    }
}