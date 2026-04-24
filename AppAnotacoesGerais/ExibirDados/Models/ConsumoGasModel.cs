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

    private decimal _preco;
    public decimal Preco
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
}
