using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.AnotacoesGerais;

public class EditarAnotacaoGeralViewModel : ViewModelBase//Não mudar essa classe porque senão dá erro no Dispacher.
{
    public AnotacaoGeralRepositorio _anotacaoGeralRepositorio = new();

    public AnotacaoGeralModel AnotacaoGeralModel { get; set; } = new();
    public CategoriaModel CategoriaModel { get; set; } = new();
    public SubcategoriaModel SubcategoriaModel { get; set; } = new();
    public NomeDescricaoModel NomeDescricaoModel { get; set; } = new();

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private Categoria _categoriaSelecionadaEditar;
    public Categoria CategoriaSelecionadaEditar
    {
        get => _categoriaSelecionadaEditar;
        set
        {
            if (_categoriaSelecionadaEditar != value)
            {
                _categoriaSelecionadaEditar = value;
                OnPropertyChanged(nameof(CategoriaSelecionadaEditar));

                ObterListaDeSubcategorias();
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private Subcategoria _subcategoriaSelecionadaEditar = new();
    public Subcategoria SubcategoriaSelecionadaEditar
    {
        get => _subcategoriaSelecionadaEditar;
        set
        {
            if (_subcategoriaSelecionadaEditar != value)
            {
                _subcategoriaSelecionadaEditar = value;
                OnPropertyChanged(nameof(SubcategoriaSelecionadaEditar));

                ObterListaDeNomeDescricao();
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Subcategorias e o ComboBox de NomeDescricao.
    private NomeDescricao _nomeDescricaoSelecionadaEditar = new();
    public NomeDescricao NomeDescricaoSelecionadaEditar
    {
        get => _nomeDescricaoSelecionadaEditar;
        set
        {
            if (_nomeDescricaoSelecionadaEditar != value)
            {
                _nomeDescricaoSelecionadaEditar = value;
                OnPropertyChanged(nameof(NomeDescricaoSelecionadaEditar));
            }
        }
    }



    //Método do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private void ObterListaDeSubcategorias()
    {
        if (CategoriaSelecionadaEditar != null)
        {
            SubcategoriaModel.ListaDeSubcategorias = [.. SubcategoriaRepositorio.ObterSubcategoriasPorId(CategoriaSelecionadaEditar.Id) ?? []];
            SubcategoriaModel.IndiceSelecionadoSubcategoria = -1;
        }
        else
        {
            SubcategoriaModel.ListaDeSubcategorias = [];
        }
    }

    //Método do evento: "SelectionChanged" entre o ComboBox de Sucategorias e o ComboBox de NomeDescricao.
    private void ObterListaDeNomeDescricao()
    {
        if (SubcategoriaSelecionadaEditar != null)
        {
            NomeDescricaoModel.ListaDoNomeDescricao = [.. NomeDescricaoRepositorio.ObterNomeDescricaoPorId(SubcategoriaSelecionadaEditar.Id) ?? []];
        }
        else
        {
            NomeDescricaoModel.ListaDoNomeDescricao = [];
        }
    }

    private ICommand _comandoAdicionarAnotacaoGeral;
    public ICommand ComandoAdicionarAnotacaoGeral
    {
        get
        {
            _comandoAdicionarAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de AnotacaoGeralModel para evitar
                // modificar a instância vinculada à interface do usuário.
                AnotacaoGeralModel anotacaoGeralModel = new();
                anotacaoGeralModel = AnotacaoGeralModel;

                if (anotacaoGeralModel.Id == 0 && !string.IsNullOrWhiteSpace(anotacaoGeralModel.Descricao))
                {
                    try
                    {
                        CategoriaRepositorio categoriaRepositorio = new();
                        AnotacaoGeral anotacaoGeral = new()
                        {
                            NomeCategoria = anotacaoGeralModel.NomeCategoria,
                            NomeSubcategoria = anotacaoGeralModel.NomeSubcategoria,
                            NomeDaDescricao = anotacaoGeralModel.NomeDaDescricao,
                            Descricao = anotacaoGeralModel.Descricao,
                            Data = anotacaoGeralModel.Data,
                        };
                        _anotacaoGeralRepositorio.Adicionar(anotacaoGeral);
                        Mensagens.SucessoAoAdicionar(anotacaoGeral.Id);
                        LimparDados();
                    }
                    catch (Exception ex)
                    {
                        Mensagens.NomeDoMetodo = "ComandoAdicionarAnotacaoGeral";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                    }
                }
                else if (anotacaoGeralModel.Id > 0 && !string.IsNullOrWhiteSpace(anotacaoGeralModel.Descricao))
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
            return _comandoAdicionarAnotacaoGeral;
        }
    }

    private ICommand _comandoEditarAnotacaoGeral;
    public ICommand ComandoEditarAnotacaoGeral
    {
        get
        {
            _comandoEditarAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de AnotacaoGeralModel para evitar
                // modificar a instância vinculada à interface do usuário.
                AnotacaoGeralModel anotacaoGeralModel = new();
                anotacaoGeralModel = AnotacaoGeralModel;

                if (anotacaoGeralModel.Id > 0 && !string.IsNullOrWhiteSpace(anotacaoGeralModel.Descricao))
                {
                    try
                    {
                        CategoriaRepositorio categoriaRepositorio = new();
                        AnotacaoGeral anotacaoGeral = new()
                        {
                            Id = anotacaoGeralModel.Id,
                            NomeCategoria = anotacaoGeralModel.NomeCategoria,
                            NomeSubcategoria = anotacaoGeralModel.NomeSubcategoria,
                            NomeDaDescricao = anotacaoGeralModel.NomeDaDescricao,
                            Descricao = anotacaoGeralModel.Descricao,
                            Data = anotacaoGeralModel.Data,
                        };
                        _anotacaoGeralRepositorio.Editar(anotacaoGeral);
                        Mensagens.SucessoAoEditar(anotacaoGeral.Id);
                        LimparDados();
                    }
                    catch (Exception ex)
                    {
                        Mensagens.NomeDoMetodo = "ComandoEditarAnotacaoGeral";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
                    }
                }
                else if (anotacaoGeralModel.Id == 0 && !string.IsNullOrWhiteSpace(anotacaoGeralModel.Descricao))
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
            return _comandoEditarAnotacaoGeral;
        }
    }

    private ICommand _comandoLimparAnotacaoGeral;
    public ICommand ComandoLimparAnotacaoGeral
    {
        get
        {
            _comandoLimparAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                AnotacaoGeralModel.Id = 0;
                AnotacaoGeralModel.NomeCategoria = null;
                AnotacaoGeralModel.NomeSubcategoria = null;
                AnotacaoGeralModel.NomeDaDescricao = null;
                AnotacaoGeralModel.Descricao = null;
                AnotacaoGeralModel.Data = DateTime.Now;
            });
            return _comandoLimparAnotacaoGeral;
        }
    }

    public void LimparDados()
    {
        AnotacaoGeralModel.Id = 0;
        AnotacaoGeralModel.NomeCategoria = null;
        AnotacaoGeralModel.NomeSubcategoria = null;
        AnotacaoGeralModel.NomeDaDescricao = null;
    }
}
