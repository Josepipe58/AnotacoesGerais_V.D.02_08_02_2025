using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Windows;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.NomeDescricaoVM;

public partial class NomeDescricaoViewModel// NomeDescricaoGerenciar
{
    public void Adicionar(NomeDescricaoModel nomeDescricaoModel)
    {

        if (nomeDescricaoModel.Id == 0 && !string.IsNullOrWhiteSpace(nomeDescricaoModel.NomeDaDescricao))
        {
            try
            {
                NomeDescricaoRepositorio nomeDaDescricaoRepositorio = new();
                NomeDescricao nomeDaDescricao = new()
                {
                    NomeDaDescricao = nomeDescricaoModel.NomeDaDescricao,
                    CategoriaId = nomeDescricaoModel.CategoriaId,
                    SubcategoriaId = nomeDescricaoModel.SubcategoriaId,
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
        else if (nomeDescricaoModel.Id > 0 && !string.IsNullOrWhiteSpace(nomeDescricaoModel.NomeDaDescricao))
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

    public void Editar(NomeDescricaoModel nomeDescricaoModel)
    {

        if (nomeDescricaoModel.Id > 0 && !string.IsNullOrWhiteSpace(nomeDescricaoModel.NomeDaDescricao))
        {
            try
            {
                NomeDescricaoRepositorio nomeDaDescricaoRepositorio = new();
                NomeDescricao nomeDaDescricao = new()
                {
                    Id = nomeDescricaoModel.Id,
                    NomeDaDescricao = nomeDescricaoModel.NomeDaDescricao,
                    CategoriaId = nomeDescricaoModel.CategoriaId,
                    SubcategoriaId = nomeDescricaoModel.SubcategoriaId,
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
        else if (nomeDescricaoModel.Id == 0 && !string.IsNullOrWhiteSpace(nomeDescricaoModel.NomeDaDescricao))
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

    public void Excluir(NomeDescricaoModel nomeDescricaoModel)
    {
        if (nomeDescricaoModel.Id > 0 && !string.IsNullOrWhiteSpace(nomeDescricaoModel.NomeDaDescricao))
        {
            MessageBoxResult resultado = Mensagens.ConfirmarExcluir(nomeDescricaoModel.Id);
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
                    Id = nomeDescricaoModel.Id
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
        else if (nomeDescricaoModel.Id == 0 && !string.IsNullOrWhiteSpace(nomeDescricaoModel.NomeDaDescricao))
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
    private void DuploClickNomeDescricao(object parameter)
    {
        if (parameter is NomeDescricao nomeDaDescricao)
        {
            NomeDescricaoModel.Id = nomeDaDescricao.Id;
            NomeDescricaoModel.NomeDaDescricao = nomeDaDescricao.NomeDaDescricao;
            NomeDescricaoModel.CategoriaId = nomeDaDescricao.CategoriaId;
            NomeDescricaoModel.NomeCategoria = nomeDaDescricao.NomeCategoria;
            NomeDescricaoModel.SubcategoriaId = nomeDaDescricao.SubcategoriaId;
            NomeDescricaoModel.NomeSubcategoria = nomeDaDescricao.NomeSubcategoria;
        }
    }

    public void LimparDados()
    {
        NomeDescricaoModel.Id = 0;
        NomeDescricaoModel.NomeDaDescricao = null;

        var listaDeSubcategorias = NomeDescricaoRepositorio.ObterNomeDescricao().ToList() ?? [];

        //Carregar DataGrid do Nome da Descrição.        
        ListaDoNomeDescricao = new ObservableCollection<NomeDescricao>(listaDeSubcategorias);
    }

    public void Atualizar()
    {
        NomeDescricaoModel.SubcategoriaId = 1;
        NomeDescricaoModel.CategoriaId = 1;
        TextoPesquisa = null;
        LimparDados();
    }
}
