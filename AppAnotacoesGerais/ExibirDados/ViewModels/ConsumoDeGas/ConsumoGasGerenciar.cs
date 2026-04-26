using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Models;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.ConsumoDeGas;

public partial class ConsumoGasViewModel// ConsumoDeGasGerenciar
{
    /*
    public void Adicionar(ConsumoGasModel consumoGasModel)
    {

        if (consumoGasModel.Id == 0 && consumoGasModel.DiasConsumo > 0 && consumoGasModel.Preco >= 0
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
                    Preco = consumoGasModel.Preco,
                    Fornecedor = consumoGasModel.Fornecedor
                };
                consumoGasRepositorio.Adicionar(consumoGas);
                Mensagens.SucessoAoAdicionar(consumoGas.Id);
                LimparDados();
            }
            catch (Exception ex)
            {
                Mensagens.NomeDoMetodo = "Adicionar";
                Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            }
        }
        else if (consumoGasModel.Id > 0 && consumoGasModel.DiasConsumo > 0 && consumoGasModel.Preco > 0
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
    }

    public void Editar(ConsumoGasModel consumoGasModel)
    {

        if (consumoGasModel.Id > 0 && consumoGasModel.DiasConsumo > 0 && consumoGasModel.Preco > 0
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
                    Preco = consumoGasModel.Preco,
                    Fornecedor = consumoGasModel.Fornecedor
                };
                consumoGasRepositorio.Editar(consumoGas);
                Mensagens.SucessoAoEditar(consumoGas.Id);
                LimparDados();
            }
            catch (Exception ex)
            {
                Mensagens.NomeDoMetodo = "Editar";
                Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            }
        }
        else if (consumoGasModel.Id == 0 && consumoGasModel.DiasConsumo > 0 && consumoGasModel.Preco >= 0
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
    }

    public void Excluir(ConsumoGasModel consumoGasModel)
    {
        if (consumoGasModel.Id > 0)
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
                Mensagens.NomeDoMetodo = "Excluir";
                Mensagens.ErroDeExcecaoENomeDoMetodo(erro, Mensagens.NomeDoMetodo);
                return;
            }
        }
        else if (consumoGasModel.Id == 0)
        {
            Mensagens.ErroAoEditarOuExcluir();
            return;
        }
        else
        {
            Mensagens.PreencherCampoVazio();
            return;
        }
    }
    
    //Método do evento MouseDoubleClick no DataGrid.
    private void DuploClickConsumoGas(object parameter)
    {
        if (parameter is ConsumoGas consumoGas)
        {
            ConsumoGasModel.Id = consumoGas.Id;
            ConsumoGasModel.DataAnterior = consumoGas.DataTroca;
            ConsumoGasModel.DataTroca = consumoGas.DataTroca;
            ConsumoGasModel.DiasConsumo = consumoGas.DiasConsumo;
            ConsumoGasModel.DataCompra = consumoGas.DataCompra;
            ConsumoGasModel.Preco = consumoGas.Preco;
            ConsumoGasModel.Fornecedor = consumoGas.Fornecedor;
        }
    }*/

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

    public void Atualizar()
    {
        LimparDados();
    }
}
