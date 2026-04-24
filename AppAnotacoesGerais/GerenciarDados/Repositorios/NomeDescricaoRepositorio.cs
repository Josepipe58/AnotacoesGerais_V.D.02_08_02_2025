using AppAnotacoesGerais.AcessarDados;
using AppAnotacoesGerais.AcessarDados.Entidades;

namespace AppAnotacoesGerais.GerenciarDados.Repositorios;

public class NomeDescricaoRepositorio : Repositorio<NomeDescricao>
{
    public static List<NomeDescricao> ObterNomeDescricao()
    {
        try
        {
            using Contexto contexto = new();
            var listaDoNomeDaDescricao = contexto.TSubcategoria.Join(contexto.TCategoria,
                sc => sc.CategoriaId,
                c => c.Id,
                (sc, c) => new { sc, c })
                .Join(contexto.TNomeDescricao,
                scc => scc.sc.Id,
                nd => nd.SubcategoriaId,
                (scc, nd) => new NomeDescricao
                {
                    Id = nd.Id,
                    NomeDaDescricao = nd.NomeDaDescricao,
                    CategoriaId = nd.CategoriaId,
                    NomeCategoria = nd.Categoria.NomeCategoria,
                    SubcategoriaId = nd.SubcategoriaId,
                    NomeSubcategoria = nd.Subcategoria.NomeSubcategoria
                }).OrderByDescending(sc => sc.Id).ToList();

            return [.. listaDoNomeDaDescricao];
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ObterNomeDescricao";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);

            return [];
        }
    }

    public static List<NomeDescricao> ObterNomeDescricaoPorId(int id)
    {
        try
        {
            using Contexto contexto = new();
            var listaDoNomeDaDescricao = contexto.TSubcategoria.Join(contexto.TCategoria,
                sc => sc.CategoriaId,
                c => c.Id,
                (sc, c) => new { sc, c })
                .Join(contexto.TNomeDescricao,
                scc => scc.sc.Id,
                nd => nd.CategoriaId,
                (scc, nd) => new NomeDescricao
                {
                    Id = nd.Id,
                    NomeDaDescricao = nd.NomeDaDescricao,
                    CategoriaId = nd.CategoriaId,
                    NomeCategoria = nd.NomeCategoria,
                    SubcategoriaId = nd.SubcategoriaId,
                    NomeSubcategoria = nd.NomeSubcategoria
                }).Where(sc => sc.SubcategoriaId == id).OrderByDescending(sc => sc.Id).ToList();

            return [.. listaDoNomeDaDescricao];
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ObterNomeDescricaoPorId";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);

            return [];
        }
    }
}
