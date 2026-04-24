using AppAnotacoesGerais.ExibirDados.Comandos;
using System;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.Subcategorias;

public partial class SubcategoriaViewModel// SubcategoriaComandos
{
    private ICommand _comandoAdicionarSubcategoria;
    public ICommand ComandoAdicionarSubcategoria
    {
        get
        {
            _comandoAdicionarSubcategoria ??= new RelayCommand<object>(param => Adicionar(SubcategoriaModel));
            return _comandoAdicionarSubcategoria;
        }
    }

    private ICommand _comandoEditarSubcategoria;
    public ICommand ComandoEditarSubcategoria
    {
        get
        {
            _comandoEditarSubcategoria ??= new RelayCommand<object>(param => Editar(SubcategoriaModel));
            return _comandoEditarSubcategoria;
        }
    }

    private ICommand _comandoExcluirSubcategoria;
    public ICommand ComandoExcluirSubcategoria
    {
        get
        {
            _comandoExcluirSubcategoria ??= new RelayCommand<object>(param => Excluir(SubcategoriaModel));
            return _comandoExcluirSubcategoria;
        }
    }

    private ICommand _comandoAtualizarSubcategoria;
    public ICommand ComandoAtualizarSubcategoria
    {
        get
        {
            _comandoAtualizarSubcategoria ??= new RelayCommand<object>(param => Atualizar());
            return _comandoAtualizarSubcategoria;
        }
    }

    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickSubcategoria;
    public ICommand ComandoDuploClickSubcategoria
    {
        get
        {
            _comandoDuploClickSubcategoria ??= new RelayCommand<object>(param => DuploClickSubcategoria(param));
            return _comandoDuploClickSubcategoria;
        }
    }
}
