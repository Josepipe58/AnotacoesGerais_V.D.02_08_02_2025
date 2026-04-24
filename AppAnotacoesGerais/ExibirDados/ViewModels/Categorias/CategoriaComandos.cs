using AppAnotacoesGerais.ExibirDados.Comandos;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.Categorias;

public partial class CategoriaViewModel//CategoriaComandos
{
    private ICommand _comandoAdicionarCategoria;
    public ICommand ComandoAdicionarCategoria
    {
        get
        {
            _comandoAdicionarCategoria ??= new RelayCommand<object>(param => Adicionar(CategoriaModel));
            return _comandoAdicionarCategoria;
        }
    }

    private ICommand _comandoEditarCategoria;
    public ICommand ComandoEditarCategoria
    {
        get
        {
            _comandoEditarCategoria ??= new RelayCommand<object>(param => Editar(CategoriaModel));
            return _comandoEditarCategoria;
        }
    }

    private ICommand _comandoExcluirCategoria;
    public ICommand ComandoExcluirCategoria
    {
        get
        {
            _comandoExcluirCategoria ??= new RelayCommand<object>(param => Excluir(CategoriaModel));
            return _comandoExcluirCategoria;
        }
    }

    private ICommand _comandoAtualizarCategoria;
    public ICommand ComandoAtualizarCategoria
    {
        get
        {
            _comandoAtualizarCategoria ??= new RelayCommand<object>(param => Atualizar());
            return _comandoAtualizarCategoria;
        }
    }

    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickCategoria;
    public ICommand ComandoDuploClickCategoria
    {
        get
        {
            _comandoDuploClickCategoria ??= new RelayCommand<object>(param => DuploClickCategoria(param));
            return _comandoDuploClickCategoria;
        }
    }
}
