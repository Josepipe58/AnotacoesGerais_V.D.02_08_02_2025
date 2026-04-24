using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Windows;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.Categorias;

public partial class CategoriaViewModel// CategoriaGerenciar
{
    public void Adicionar(CategoriaModel categoriaModel)
    {

        if (categoriaModel.Id == 0 && !string.IsNullOrWhiteSpace(categoriaModel.NomeCategoria))
        {
            try
            {
                CategoriaRepositorio categoriaRepositorio = new();
                Categoria categoria = new()
                {
                    NomeCategoria = categoriaModel.NomeCategoria
                };
                categoriaRepositorio.Adicionar(categoria);
                Mensagens.SucessoAoAdicionar(categoria.Id);
                LimparDados();
            }
            catch (Exception ex)
            {
                Mensagens.NomeDoMetodo = "Adicionar";
                Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            }
        }
        else if (categoriaModel.Id > 0 && !string.IsNullOrWhiteSpace(categoriaModel.NomeCategoria))
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

    public void Editar(CategoriaModel categoriaModel)
    {

        if (categoriaModel.Id > 0 && !string.IsNullOrWhiteSpace(categoriaModel.NomeCategoria))
        {
            try
            {
                CategoriaRepositorio categoriaRepositorio = new();
                Categoria categoria = new()
                {
                    Id = categoriaModel.Id,
                    NomeCategoria = categoriaModel.NomeCategoria
                };
                categoriaRepositorio.Editar(categoria);
                Mensagens.SucessoAoEditar(categoria.Id);
                LimparDados();
            }
            catch (Exception ex)
            {
                Mensagens.NomeDoMetodo = "Editar";
                Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            }
        }
        else if (categoriaModel.Id == 0 && !string.IsNullOrWhiteSpace(categoriaModel.NomeCategoria))
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

    public void Excluir(CategoriaModel categoriaModel)
    {
        if (categoriaModel.Id > 0 && !string.IsNullOrWhiteSpace(categoriaModel.NomeCategoria))
        {
            MessageBoxResult resultado = Mensagens.ConfirmarExcluir(categoriaModel.Id);
            if (resultado == MessageBoxResult.No)
            {
                LimparDados();
                return;
            }
            try
            {
                CategoriaRepositorio categoriaRepositorio = new();
                Categoria categoria = new()
                {
                    Id = categoriaModel.Id
                };
                categoriaRepositorio.Excluir(categoria);
                Mensagens.SucessoAoExcluir(categoria.Id);
                LimparDados();
            }
            catch (Exception erro)
            {
                Mensagens.NomeDoMetodo = "Excluir";
                Mensagens.ErroDeExcecaoENomeDoMetodo(erro, Mensagens.NomeDoMetodo);
                return;
            }
        }
        else if (categoriaModel.Id == 0 && !string.IsNullOrWhiteSpace(categoriaModel.NomeCategoria))
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
    private void DuploClickCategoria(object parameter)
    {
        if (parameter is Categoria categoria)
        {
            CategoriaModel.Id = categoria.Id;
            CategoriaModel.NomeCategoria = categoria.NomeCategoria;
        }
    }

    public void LimparDados()
    {
        CategoriaModel.Id = 0;
        CategoriaModel.NomeCategoria = null;

        var listaDeCategorias = _categoriaRepositorio.ObterListaDeTodos().ToList() ?? [];
        //Carregar DataGrid de Categorias.        
         CategoriaModel.ListaDeCategorias = new ObservableCollection<Categoria>(listaDeCategorias);       
    }

    public void Atualizar()
    {
        LimparDados();
    }
}
