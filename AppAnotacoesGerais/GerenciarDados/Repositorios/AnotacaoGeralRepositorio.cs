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
    public static bool VerificarRegistros(int id)
    {
        try
        {
            if (id <= 0) return false;
            using Contexto contexto = new();
            return contexto.TAnotacaoGeral.Any(x => x.Id == id);
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "VerificarRegistros";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return false;
        }
    }

    public int ContadorRegistros()
    {
        try
        {
            int retorno = ObterListaDeTodos().Count;
            return retorno;

        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ContadorRegistros";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return 0;
        }
    }
}
