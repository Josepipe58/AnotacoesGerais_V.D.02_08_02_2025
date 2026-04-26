using AppAnotacoesGerais.AcessarDados;
using AppAnotacoesGerais.AcessarDados.Entidades;

namespace AppAnotacoesGerais.GerenciarDados.Repositorios;

public class ConsumoGasRepositorio : Repositorio<ConsumoGas>
{
    /*
    public static List<ConsumoGas> ObterListaDeConsumoGas()
    {
        try
        {
            using Contexto contexto = new();
            var listaDeConsumoGas = contexto.TConsumoGas.Select(d => new ConsumoGas()
            {
                Id = d.Id,
                DataCompra = d.DataCompra,
                DataTroca = d.DataTroca,
                DiasConsumo = d.DiasConsumo,
                Preco = d.Preco,
                Fornecedor = d.Fornecedor
            }).OrderByDescending(d => d.Id);

            return [.. listaDeConsumoGas];
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ObterListaDeConsumoGas";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return [];
        }
        
    }*/
}
