using AppAnotacoesGerais.AcessarDados;
using AppAnotacoesGerais.AcessarDados.Entidades;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.GerenciarDados.Repositorios;

public class SubcategoriaRepositorio : Repositorio<Subcategoria>
{
    public static List<Subcategoria> ObterSubcategorias()
    {
        try
        {
            using Contexto contexto = new();
            var listaDeSubcategorias = contexto.TSubcategoria.Join(contexto.TCategoria,
                sc => sc.CategoriaId,
                c => c.Id,
                (sc, c) => new Subcategoria
                {
                    Id = sc.Id,
                    NomeSubcategoria = sc.NomeSubcategoria,
                    CategoriaId = sc.CategoriaId,
                    NomeCategoria = c.NomeCategoria,

                }).OrderByDescending(sc => sc.Id).ToList();

            return [.. listaDeSubcategorias];

        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ObterSubcategorias";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);

            return [];
        }
    }

    public static ObservableCollection<Subcategoria> ObterSubcategoriasPorId(int id)
    {
        try
        {
            using Contexto contexto = new();
            var listaSubcategorias = contexto.TCategoria
                .Join(contexto.TSubcategoria,
                c => c.Id,
                sc => sc.CategoriaId,
                (c, sc) => new Subcategoria
                {
                    Id = sc.Id,
                    NomeSubcategoria = sc.NomeSubcategoria,
                    CategoriaId = sc.CategoriaId,
                    NomeCategoria = c.NomeCategoria,
                }).Where(sc => sc.CategoriaId == id).OrderBy(sc => sc.NomeSubcategoria);

            return [.. listaSubcategorias];
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ObterSubcategoriasPorId";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);

            return [];
        }
    }
}
