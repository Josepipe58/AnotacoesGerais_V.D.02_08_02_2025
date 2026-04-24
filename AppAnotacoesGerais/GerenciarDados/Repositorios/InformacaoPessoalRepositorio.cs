using AppAnotacoesGerais.AcessarDados;
using AppAnotacoesGerais.AcessarDados.Entidades;

namespace AppAnotacoesGerais.GerenciarDados.Repositorios;

public class InformacaoPessoalRepositorio : Repositorio<InformacaoPessoal>
{
    public static List<InformacaoPessoal> ObterInformacaoPessoal()
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

    public static List<InformacaoPessoal> ObterInformacaoPessoalPorId(int id)
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
    public bool VerificarRegistros(int id)
    {
        try
        {
            using Contexto contexto = new();
            var verificarConsulta = contexto.TInformacaoPessoal.Select(x => x.Id == id); //SelecionarPK(id);
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
