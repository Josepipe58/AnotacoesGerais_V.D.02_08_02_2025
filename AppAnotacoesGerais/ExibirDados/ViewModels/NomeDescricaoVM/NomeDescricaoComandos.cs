using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.NomeDescricaoVM;

public partial class NomeDescricaoViewModel//NomeDescricaoComandos
{
    private ICommand _comandoAdicionarNomeDescricao;
    public ICommand ComandoAdicionarNomeDescricao
    {
        get
        {
            _comandoAdicionarNomeDescricao ??= new RelayCommand<object>(param => 
            {
                // Criar uma nova instância de NomeDescricaoModel para evitar
                // modificar a instância vinculada à interface do usuário.
                NomeDescricaoModel nomeDescricaoModel = new();
                nomeDescricaoModel = NomeDescricaoModel;

                if (nomeDescricaoModel.Id == 0 && !string.IsNullOrWhiteSpace(nomeDescricaoModel.NomeDaDescricao))
                {
                    try
                    {
                        GerenciarDados.Repositorios.NomeDescricaoRepositorio nomeDaDescricaoRepositorio = new();
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
                        Mensagens.NomeDoMetodo = "ComandoAdicionarNomeDescricao";
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
            });
            return _comandoAdicionarNomeDescricao;
        }
    }

    private ICommand _comandoEditarNomeDescricao;
    public ICommand ComandoEditarNomeDescricao
    {
        get
        {
            _comandoEditarNomeDescricao ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de NomeDescricaoModel para evitar
                // modificar a instância vinculada à interface do usuário.
                NomeDescricaoModel nomeDescricaoModel = new();
                nomeDescricaoModel = NomeDescricaoModel;

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
            });
            return _comandoEditarNomeDescricao;
        }
    }

    private ICommand _comandoExcluirNomeDescricao;
    public ICommand ComandoExcluirNomeDescricao
    {
        get
        {
            _comandoExcluirNomeDescricao ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de NomeDescricaoModel para evitar
                // modificar a instância vinculada à interface do usuário.
                NomeDescricaoModel nomeDescricaoModel = new();
                nomeDescricaoModel = NomeDescricaoModel;

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
            });
            return _comandoExcluirNomeDescricao;
        }
    }

    private ICommand _comandoAtualizarNomeDescricao;
    public ICommand ComandoAtualizarNomeDescricao
    {
        get
        {
            _comandoAtualizarNomeDescricao ??= new RelayCommand<object>(param =>
            {
                NomeDescricaoModel.SubcategoriaId = 1;
                NomeDescricaoModel.CategoriaId = 1;
                TextoPesquisa = null;
                LimparDados();
            });
            return _comandoAtualizarNomeDescricao;
        }
    }

    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickNomeDescricao;
    public ICommand ComandoDuploClickNomeDescricao
    {
        get
        {
            _comandoDuploClickNomeDescricao ??= new RelayCommand<object>(param =>
            {
                if (param is NomeDescricao nomeDaDescricao)
                {
                    NomeDescricaoModel.Id = nomeDaDescricao.Id;
                    NomeDescricaoModel.NomeDaDescricao = nomeDaDescricao.NomeDaDescricao;
                    NomeDescricaoModel.CategoriaId = nomeDaDescricao.CategoriaId;
                    NomeDescricaoModel.NomeCategoria = nomeDaDescricao.NomeCategoria;
                    NomeDescricaoModel.SubcategoriaId = nomeDaDescricao.SubcategoriaId;
                    NomeDescricaoModel.NomeSubcategoria = nomeDaDescricao.NomeSubcategoria;
                }
            });
            return _comandoDuploClickNomeDescricao;
        }
    }
}
