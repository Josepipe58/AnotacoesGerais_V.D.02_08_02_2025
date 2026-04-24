using AppAnotacoesGerais.AcessarDados;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.GerenciarDados.Repositorios;

public abstract class Repositorio<T> : IDisposable where T : class
{
    public readonly Contexto _contexto;

    public bool Salvar { get; set; } = true;

    public Repositorio(bool save = true)
    {
        Salvar = save;
        _contexto = new Contexto();
    }

    public ObservableCollection<T> ObterListaDeTodos()
    {
        try
        {
            return [.. _contexto.Set<T>().AsNoTracking()];
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ObterListaDeTodos";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return [];
        }
    }

    public void Adicionar(T entity)
    {
        _contexto.Set<T>().Add(entity);
        if (Salvar)
        {
            SalvarAlteracoes();
        }
    }

    public void Editar(T entity)
    {
        _contexto.Entry(entity).State = EntityState.Modified;
        if (Salvar)
        {
            SalvarAlteracoes();
        }
    }

    public void Excluir(T entity)
    {
        _contexto.Set<T>().Remove(entity);
        if (Salvar)
        {
            SalvarAlteracoes();
        }
    }

    public void SalvarAlteracoes()
    {
        _contexto.SaveChanges();
    }

    public void Dispose()
    {
        // limpar conexões do Banco de Dados.
        _contexto.Dispose();
        GC.SuppressFinalize(this);
    }
}
