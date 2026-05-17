using AppAnotacoesGerais.AcessarDados;
using AppAnotacoesGerais.AcessarDados.Entidades;

namespace AppAnotacoesGerais.GerenciarDados.Repositorios;

public class InformacaoPessoalRepositorio : Repositorio<InformacaoPessoal>
{
    public static List<InformacaoPessoal> ObterInformacoesPessoais()
    {
        try
        {
            using Contexto contexto = new();
            var listaDeInformacaoPessoal = contexto.TInformacaoPessoal.ToList();

            return [.. listaDeInformacaoPessoal];
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ObterInformacaoPessoal";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);

            return [];
        }
    }

    public static List<InformacaoPessoal> ObterInformacoesPessoaisPorId(int id)
    {
        try
        {
            using Contexto contexto = new();
            var listaDeInformacoesPessoais = contexto.TInformacaoPessoal.Select(d => new InformacaoPessoal()
            {
                Id = d.Id,
                Titulo = d.Titulo,
                Descricao = d.Descricao
            }).Where(an => an.Id == id).OrderByDescending(d => d.Id).ToList();

            return [.. listaDeInformacoesPessoais];
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ObterInformacaoPessoalPorId";
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

            return contexto.TInformacaoPessoal.Any(x => x.Id == id);
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "VerificarRegistros";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);

            return false;
        }
    }
}
