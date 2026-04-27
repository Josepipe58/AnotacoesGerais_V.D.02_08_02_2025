using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Windows;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.Subcategorias;

public partial class SubcategoriaViewModel// SubcategoriaComandos
{
    private ICommand _comandoAdicionarSubcategoria;
    public ICommand ComandoAdicionarSubcategoria
    {
        get
        {
            _comandoAdicionarSubcategoria ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de SubcategoriaModel para evitar
                // modificar a instância vinculada à interface do usuário.
                SubcategoriaModel subcategoriaModel = new();
                subcategoriaModel = SubcategoriaModel;

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
            });
            return _comandoAdicionarSubcategoria;
        }
    }

    private ICommand _comandoEditarSubcategoria;
    public ICommand ComandoEditarSubcategoria
    {
        get
        {
            _comandoEditarSubcategoria ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de SubcategoriaModel para evitar
                // modificar a instância vinculada à interface do usuário.
                SubcategoriaModel subcategoriaModel = new();
                subcategoriaModel = SubcategoriaModel;

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
            });
            return _comandoEditarSubcategoria;
        }
    }

    private ICommand _comandoExcluirSubcategoria;
    public ICommand ComandoExcluirSubcategoria
    {
        get
        {
            _comandoExcluirSubcategoria ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de SubcategoriaModel para evitar
                // modificar a instância vinculada à interface do usuário.
                SubcategoriaModel subcategoriaModel = new();
                subcategoriaModel = SubcategoriaModel;

                if (subcategoriaModel.Id > 0 && !string.IsNullOrWhiteSpace(subcategoriaModel.NomeSubcategoria))
                {
                    MessageBoxResult resultado = Mensagens.ConfirmarExcluir(subcategoriaModel.Id);
                    if (resultado == MessageBoxResult.No)
                    {
                        LimparDados();
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
            });
            return _comandoExcluirSubcategoria;
        }
    }

    private ICommand _comandoAtualizarSubcategoria;
    public ICommand ComandoAtualizarSubcategoria
    {
        get
        {
            _comandoAtualizarSubcategoria ??= new RelayCommand<object>(param =>
            {
                SubcategoriaModel.CategoriaId = 1;
                TextoPesquisa = null;
                LimparDados();
            });
            return _comandoAtualizarSubcategoria;
        }
    }

    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickSubcategoria;
    public ICommand ComandoDuploClickSubcategoria
    {
        get
        {
            _comandoDuploClickSubcategoria ??= new RelayCommand<object>(param =>
            {
                if (param is Subcategoria subcategoria)
                {
                    SubcategoriaModel.Id = subcategoria.Id;
                    SubcategoriaModel.NomeSubcategoria = subcategoria.NomeSubcategoria;
                    SubcategoriaModel.CategoriaId = subcategoria.CategoriaId;
                    SubcategoriaModel.NomeCategoria = subcategoria.NomeCategoria;
                }
            });
            return _comandoDuploClickSubcategoria;
        }
    }
}
