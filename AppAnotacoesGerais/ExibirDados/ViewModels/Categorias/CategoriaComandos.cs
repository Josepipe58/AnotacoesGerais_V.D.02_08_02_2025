using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.Categorias;

public partial class CategoriaViewModel//CategoriaComandos
{
    private ICommand _comandoAdicionarCategoria;
    public ICommand ComandoAdicionarCategoria
    {
        get
        {
            _comandoAdicionarCategoria ??= new RelayCommand<object>(param => 
            {
                // Criar uma nova instância de CategoriaModel para evitar
                // modificar a instância vinculada à interface do usuário.
                CategoriaModel categoriaModel = new();
                categoriaModel = CategoriaModel;

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
                        Mensagens.NomeDoMetodo = "ComandoAdicionarCategoria";
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
            });
            return _comandoAdicionarCategoria;
        }
    }

    private ICommand _comandoEditarCategoria;
    public ICommand ComandoEditarCategoria
    {
        get
        {
            _comandoEditarCategoria ??= new RelayCommand<object>(param => 
            {
                // Criar uma nova instância de CategoriaModel para evitar
                // modificar a instância vinculada à interface do usuário.
                CategoriaModel categoriaModel = new();
                categoriaModel = CategoriaModel;

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
                        Mensagens.NomeDoMetodo = "ComandoEditarCategoria";
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
            });
            return _comandoEditarCategoria;
        }
    }

    private ICommand _comandoExcluirCategoria;
    public ICommand ComandoExcluirCategoria
    {
        get
        {
            _comandoExcluirCategoria ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de CategoriaModel para evitar
                // modificar a instância vinculada à interface do usuário.
                CategoriaModel categoriaModel = new();
                categoriaModel = CategoriaModel;

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
                    catch (Exception ex)
                    {
                        Mensagens.NomeDoMetodo = "ComandoExcluirCategoria";
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
            });
            return _comandoExcluirCategoria;
        }
    }

    private ICommand _comandoAtualizarCategoria;
    public ICommand ComandoAtualizarCategoria
    {
        get
        {
            _comandoAtualizarCategoria ??= new RelayCommand<object>(param =>
            {
                LimparDados();
            });
            return _comandoAtualizarCategoria;
        }
    }

    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickCategoria;
    public ICommand ComandoDuploClickCategoria
    {
        get
        {
            _comandoDuploClickCategoria ??= new RelayCommand<object>(param =>
            {
                if (param is Categoria categoria)
                {
                    CategoriaModel.Id = categoria.Id;
                    CategoriaModel.NomeCategoria = categoria.NomeCategoria;
                }
            });
            return _comandoDuploClickCategoria;
        }
    }
}
