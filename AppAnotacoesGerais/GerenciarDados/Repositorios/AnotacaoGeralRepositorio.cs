using AppAnotacoesGerais.AcessarDados;
using AppAnotacoesGerais.AcessarDados.Entidades;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.GerenciarDados.Repositorios;

public class AnotacaoGeralRepositorio : Repositorio<AnotacaoGeral>
{
    public static ObservableCollection<AnotacaoGeral> ObterAnotacoesGerais()
    {
        try
        {
            using Contexto contexto = new();
            var listaDeAnotacoesGerais = contexto.TAnotacaoGeral.Select(d => new AnotacaoGeral()
            {
                Id = d.Id,
                NomeCategoria = d.NomeCategoria,
                NomeSubcategoria = d.NomeSubcategoria,
                NomeDaDescricao = d.NomeDaDescricao,
                Descricao = d.Descricao,
                Data = d.Data,
            }).OrderByDescending(d => d.Id);

            return [.. listaDeAnotacoesGerais];
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ObterAnotacoesGerais";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return [];
        }
    }

    public static ObservableCollection<AnotacaoGeral> ObterAnotacoesGeraisPorId(int id)
    {
        try
        {
            using Contexto contexto = new();
            var listaDeAnotacoesGerais = contexto.TAnotacaoGeral.Select(d => new AnotacaoGeral()
            {
                Id = d.Id,
                NomeCategoria = d.NomeCategoria,
                NomeSubcategoria = d.NomeSubcategoria,
                NomeDaDescricao = d.NomeDaDescricao,
                Descricao = d.Descricao,
                Data = d.Data,
            }).Where(an => an.Id == id).OrderByDescending(d => d.Id);

            return [.. listaDeAnotacoesGerais];
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ObterAnotacoesGeraisPorId";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return [];
        }
    }

    //Verificar se esse método não vai dar erro.
    public bool VerificarRegistros(int id)
    {
        try
        {
            using Contexto contexto = new();
            var verificarConsulta = contexto.TAnotacaoGeral.Select(x=>x.Id == id);
            if (verificarConsulta is not null)
            {
                int retorno = Convert.ToInt32(verificarConsulta);
                return retorno != 0 && retorno > 0;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            if (id <= 0)
            {
                Mensagens.NomeDoMetodo = "VerificarRegistros";
                Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                return false;
            }
            return true;
        }
    }
}
