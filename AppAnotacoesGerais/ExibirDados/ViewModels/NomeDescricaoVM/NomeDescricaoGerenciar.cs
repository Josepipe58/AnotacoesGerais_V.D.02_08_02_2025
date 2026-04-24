using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Windows;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.NomeDescricaoVM;

public partial class NomeDescricaoViewModel// NomeDescricaoGerenciar
{
    public void Adicionar(NomeDescricaoModel nomeDaDescricaoModel)
    {

        if (nomeDaDescricaoModel.Id == 0 && !string.IsNullOrWhiteSpace(nomeDaDescricaoModel.NomeDaDescricao))
        {
            try
            {
                NomeDescricaoRepositorio nomeDaDescricaoRepositorio = new();
                NomeDescricao nomeDaDescricao = new()
                {
                    NomeDaDescricao = nomeDaDescricaoModel.NomeDaDescricao,
                    CategoriaId = nomeDaDescricaoModel.CategoriaId,
                    SubcategoriaId = nomeDaDescricaoModel.SubcategoriaId,
                };
                nomeDaDescricaoRepositorio.Adicionar(nomeDaDescricao);
                Mensagens.SucessoAoAdicionar(nomeDaDescricao.Id);
                LimparDados();
            }
            catch (Exception ex)
            {
                Mensagens.NomeDoMetodo = "Adicionar";
                Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            }
        }
        else if (nomeDaDescricaoModel.Id > 0 && !string.IsNullOrWhiteSpace(nomeDaDescricaoModel.NomeDaDescricao))
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

    public void Editar(NomeDescricaoModel nomeDaDescricaoModel)
    {

        if (nomeDaDescricaoModel.Id > 0 && !string.IsNullOrWhiteSpace(nomeDaDescricaoModel.NomeDaDescricao))
        {
            try
            {
                NomeDescricaoRepositorio nomeDaDescricaoRepositorio = new();
                NomeDescricao nomeDaDescricao = new()
                {
                    Id = nomeDaDescricaoModel.Id,
                    NomeDaDescricao = nomeDaDescricaoModel.NomeDaDescricao,
                    CategoriaId = nomeDaDescricaoModel.CategoriaId,
                    SubcategoriaId = nomeDaDescricaoModel.SubcategoriaId,
                };
                nomeDaDescricaoRepositorio.Editar(nomeDaDescricao);
                Mensagens.SucessoAoEditar(nomeDaDescricao.Id);
                LimparDados();
            }
            catch (Exception ex)
            {
                Mensagens.NomeDoMetodo = "Editar";
                Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            }
        }
        else if (nomeDaDescricaoModel.Id == 0 && !string.IsNullOrWhiteSpace(nomeDaDescricaoModel.NomeDaDescricao))
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

    public void Excluir(NomeDescricaoModel nomeDaDescricaoModel)
    {
        if (nomeDaDescricaoModel.Id > 0 && !string.IsNullOrWhiteSpace(nomeDaDescricaoModel.NomeDaDescricao))
        {
            MessageBoxResult resultado = Mensagens.ConfirmarExcluir(nomeDaDescricaoModel.Id);
            if (resultado == MessageBoxResult.No)
            {
                LimparDados();
                return;
            }
            try
            {
                NomeDescricaoRepositorio nomeDaDescricaoRepositorio = new();
                NomeDescricao nomeDaDescricao = new()
                {
                    Id = nomeDaDescricaoModel.Id
                };
                nomeDaDescricaoRepositorio.Excluir(nomeDaDescricao);
                Mensagens.SucessoAoExcluir(nomeDaDescricao.Id);
                LimparDados();
            }
            catch (Exception erro)
            {
                Mensagens.NomeDoMetodo = "Excluir";
                Mensagens.ErroDeExcecaoENomeDoMetodo(erro, Mensagens.NomeDoMetodo);
                return;
            }
        }
        else if (nomeDaDescricaoModel.Id == 0 && !string.IsNullOrWhiteSpace(nomeDaDescricaoModel.NomeDaDescricao))
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
    private void DuploClickNomeDaDescricao(object parameter)
    {
        if (parameter is NomeDescricao nomeDaDescricao)
        {
            NomeDaDescricaoModel.Id = nomeDaDescricao.Id;
            NomeDaDescricaoModel.NomeDaDescricao = nomeDaDescricao.NomeDaDescricao;
            NomeDaDescricaoModel.CategoriaId = nomeDaDescricao.CategoriaId;
            NomeDaDescricaoModel.NomeCategoria = nomeDaDescricao.NomeCategoria;
            NomeDaDescricaoModel.SubcategoriaId = nomeDaDescricao.SubcategoriaId;
            NomeDaDescricaoModel.NomeSubcategoria = nomeDaDescricao.NomeSubcategoria;
        }
    }

    public void LimparDados()
    {
        NomeDaDescricaoModel.Id = 0;
        NomeDaDescricaoModel.NomeDaDescricao = null;
        NomeDaDescricaoModel.SubcategoriaId = 0;
        NomeDaDescricaoModel.CategoriaId = 0;

        var listaDeSubcategorias = NomeDescricaoRepositorio.ObterNomeDescricao().ToList() ?? [];

        //Carregar DataGrid de Subcategorias.        
        ListaDoNomeDaDescricao = new ObservableCollection<NomeDescricao>(listaDeSubcategorias);
    }

    public void Atualizar()
    {
        //Limpar ComboBox de Categorias.
        NomeDaDescricaoModel.NomeCategoria = null;
        NomeDaDescricaoModel.NomeSubcategoria = null;
        TextoPesquisa = null;
        LimparDados();
    }
}
