using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.ExibirDados.ViewModels.AnotacoesGerais;
using AppAnotacoesGerais.ExibirDados.Views.AnotacoesGeraisView;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.AnotacoesGerais;

public class EditarAnotacaoGeralViewModel: ViewModelBase
{/*
    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickAnotacaoGeral;
    public ICommand ComandoDuploClickAnotacaoGeral
    {
        get
        {
            _comandoDuploClickAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                if (param is AnotacaoGeral anotacaoGeral)
                {
                    // Preencher um modelo que será usado como DataContext na view de edição.
                    var modeloParaEditar = new AnotacaoGeralModel
                    {
                        Id = anotacaoGeral.Id,
                        NomeCategoria = anotacaoGeral.NomeCategoria,
                        NomeSubcategoria = anotacaoGeral.NomeSubcategoria,
                        NomeDaDescricao = anotacaoGeral.NomeDaDescricao,
                        Descricao = anotacaoGeral.Descricao,
                        Data = Convert.ToDateTime(anotacaoGeral.Data.ToString("dd/MM/yyyy"))
                    };

                    // Verificar se o registro existe antes de abrir a view de edição.
                    bool existe = AnotacaoGeralRepositorio.VerificarRegistros(modeloParaEditar.Id);
                    if (!existe)
                    {
                        Mensagens.NomeDoMetodo = "VerificarRegistros";
                        Mensagens.ErroObterId(modeloParaEditar.Id, Mensagens.NomeDoMetodo);
                        return;
                    }

                    // Abrir a view de edição em um UserControl, passando o modelo clonado e o ViewModel
                    //SelecionarControleDeUsuario = new EditarAnotacaoGeralView(modeloParaEditar, null);
                    /*
                    bool? resultado = userControl?.ShowDialog();
                    if (resultado == true)
                    {
                        // A janela já realizou a persistência; atualize a lista exibida chamando consulta.
                        //AnotacaoGeralViewModel.ConsultasDeAnotacoesGerais();
                    }                  
                }

            });
            return _comandoDuploClickAnotacaoGeral;
        }
    }
    //=================================================================================
    //public CategoriaModel CategoriaModel { get; set; } = new();
    //public SubcategoriaModel SubcategoriaModel { get; set; } = new();
    //public AnotacaoGeralModel AnotacaoGeralModel { get; set; } = new();

    private ObservableCollection<NomeDescricao> _listaDoNomeDescricao;
    public ObservableCollection<NomeDescricao> ListaDoNomeDescricao
    {
        get => _listaDoNomeDescricao;
        set
        {
            if (_listaDoNomeDescricao != value)
            {
                _listaDoNomeDescricao = value;
                OnPropertyChanged(nameof(ListaDoNomeDescricao));
            }
        }
    }

    private int _indiceSelecionadoNomeDescricao;
    public int IndiceSelecionadoNomeDescricao
    {
        get => _indiceSelecionadoNomeDescricao;
        set
        {
            if (_indiceSelecionadoNomeDescricao != value)
            {
                _indiceSelecionadoNomeDescricao = value;
                OnPropertyChanged(nameof(IndiceSelecionadoNomeDescricao));
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private Categoria _categoriaSelecionada;
    public Categoria CategoriaSelecionada
    {
        get => _categoriaSelecionada;
        set
        {
            if (_categoriaSelecionada != value)
            {
                _categoriaSelecionada = value;
                OnPropertyChanged(nameof(CategoriaSelecionada));

                //Aguarde o binding ser atualizado antes de chamar os métodos.
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ObterListaDeSubcategorias();
                    ObterListaDeNomeDescricao();
                });
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private Subcategoria _subcategoriaSelecionada = new();
    public Subcategoria SubcategoriaSelecionada
    {
        get => _subcategoriaSelecionada;
        set
        {
            if (_subcategoriaSelecionada != value)
            {
                _subcategoriaSelecionada = value;
                OnPropertyChanged(nameof(SubcategoriaSelecionada));

                //Aguarde o binding ser atualizado antes de chamar os métodos.
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ObterListaDeNomeDescricao();
                });
            }
        }
    }

    //Propriedade do evento: "SelectionChanged" entre o ComboBox de Subcategorias e o ComboBox de NomeDescricao.
    private NomeDescricao _nomeDescricaoSelecionada = new();
    public NomeDescricao NomeDescricaoSelecionada
    {
        get => _nomeDescricaoSelecionada;
        set
        {
            if (_nomeDescricaoSelecionada != value)
            {
                _nomeDescricaoSelecionada = value;
                OnPropertyChanged(nameof(NomeDescricaoSelecionada));
            }
        }
    }

    //Método do evento: "SelectionChanged" entre o ComboBox de Categorias e o ComboBox de Subcategorias.
    private void ObterListaDeSubcategorias()
    {
        if (CategoriaSelecionada != null)
        {
            SubcategoriaModel.ListaDeSubcategorias = [.. SubcategoriaRepositorio.ObterSubcategoriasPorId(CategoriaSelecionada.Id) ?? []];
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
        if (SubcategoriaSelecionada != null)
        {
            ListaDoNomeDescricao = [.. NomeDescricaoRepositorio.ObterNomeDescricaoPorId(SubcategoriaSelecionada.Id) ?? []];
            IndiceSelecionadoNomeDescricao = -1;            
        }
        else
        {
            ListaDoNomeDescricao = [];
        }
    }
    */
}
