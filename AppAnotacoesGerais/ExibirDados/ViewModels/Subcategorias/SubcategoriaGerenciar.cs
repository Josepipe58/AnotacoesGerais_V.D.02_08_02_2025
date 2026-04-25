using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Windows;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.Subcategorias;

public partial class SubcategoriaViewModel// SubcategoriaGerenciar
{
    public void Adicionar(SubcategoriaModel subcategoriaModel)
    {

        if (subcategoriaModel.Id == 0 && !string.IsNullOrWhiteSpace(subcategoriaModel.NomeSubcategoria))
        {
            try
            {
                SubcategoriaRepositorio subcategoriaRepositorio = new();
                Subcategoria subcategoria = new()
                {
                    NomeSubcategoria = subcategoriaModel.NomeSubcategoria,
                    CategoriaId = subcategoriaModel.CategoriaId
                };
                subcategoriaRepositorio.Adicionar(subcategoria);
                Mensagens.SucessoAoAdicionar(subcategoria.Id);
                LimparDados();
            }
            catch (Exception ex)
            {
                Mensagens.NomeDoMetodo = "Adicionar";
                Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            }
        }
        else if (subcategoriaModel.Id > 0 && !string.IsNullOrWhiteSpace(subcategoriaModel.NomeSubcategoria))
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

    public void Editar(SubcategoriaModel subcategoriaModel)
    {

        if (subcategoriaModel.Id > 0 && !string.IsNullOrWhiteSpace(subcategoriaModel.NomeSubcategoria))
        {
            try
            {
                SubcategoriaRepositorio subcategoriaRepositorio = new();
                Subcategoria subcategoria = new()
                {
                    Id = subcategoriaModel.Id,
                    NomeSubcategoria = subcategoriaModel.NomeSubcategoria,
                    CategoriaId = subcategoriaModel.CategoriaId
                };
                subcategoriaRepositorio.Editar(subcategoria);
                Mensagens.SucessoAoEditar(subcategoria.Id);
                LimparDados();
            }
            catch (Exception ex)
            {
                Mensagens.NomeDoMetodo = "Editar";
                Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            }
        }
        else if (subcategoriaModel.Id == 0 && !string.IsNullOrWhiteSpace(subcategoriaModel.NomeSubcategoria))
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

    public void Excluir(SubcategoriaModel subcategoriaModel)
    {
        if (subcategoriaModel.Id > 0 && !string.IsNullOrWhiteSpace(subcategoriaModel.NomeSubcategoria))
        {
            MessageBoxResult resultado = Mensagens.ConfirmarExcluir(subcategoriaModel.Id);
            if (resultado == MessageBoxResult.No)
            {
                Atualizar();
                return;
            }
            try
            {
                SubcategoriaRepositorio subcategoriaRepositorio = new();
                Subcategoria subcategoria = new()
                {
                    Id = subcategoriaModel.Id
                };
                subcategoriaRepositorio.Excluir(subcategoria);
                Mensagens.SucessoAoExcluir(subcategoria.Id);
                LimparDados();
            }
            catch (Exception erro)
            {
                Mensagens.NomeDoMetodo = "Excluir";
                Mensagens.ErroDeExcecaoENomeDoMetodo(erro, Mensagens.NomeDoMetodo);
                return;
            }
        }
        else if (subcategoriaModel.Id == 0 && !string.IsNullOrWhiteSpace(subcategoriaModel.NomeSubcategoria))
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
    private void DuploClickSubcategoria(object parameter)
    {
        if (parameter is Subcategoria subcategoria)
        {
            SubcategoriaModel.Id = subcategoria.Id;
            SubcategoriaModel.NomeSubcategoria = subcategoria.NomeSubcategoria;
            SubcategoriaModel.CategoriaId = subcategoria.CategoriaId;
            SubcategoriaModel.NomeCategoria = subcategoria.NomeCategoria;
        }
    }

    public void LimparDados()
    {
        SubcategoriaModel.Id = 0;
        SubcategoriaModel.NomeSubcategoria = null;

        var listaDeSubcategorias = SubcategoriaRepositorio.ObterSubcategorias().ToList() ?? [];

        //Carregar DataGrid de Subcategorias.        
        SubcategoriaModel.ListaDeSubcategorias = new ObservableCollection<Subcategoria>(listaDeSubcategorias);
    }

    public void Atualizar()
    {
        SubcategoriaModel.NomeCategoria = CategoriaModel.ListaDeCategorias[0].NomeCategoria;
        TextoPesquisa = null;
        LimparDados();
    }
}
