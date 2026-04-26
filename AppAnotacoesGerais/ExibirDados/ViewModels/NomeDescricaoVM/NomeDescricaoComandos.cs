using AppAnotacoesGerais.ExibirDados.Comandos;
using System;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.NomeDescricaoVM;

public partial class NomeDescricaoViewModel//NomeDescricaoComandos
{
    private ICommand _comandoAdicionarNomeDescricao;
    public ICommand ComandoAdicionarNomeDescricao
    {
        get
        {
            _comandoAdicionarNomeDescricao ??= new RelayCommand<object>(param => Adicionar(NomeDescricaoModel));
            return _comandoAdicionarNomeDescricao;
        }
    }

    private ICommand _comandoEditarNomeDescricao;
    public ICommand ComandoEditarNomeDescricao
    {
        get
        {
            _comandoEditarNomeDescricao ??= new RelayCommand<object>(param => Editar(NomeDescricaoModel));
            return _comandoEditarNomeDescricao;
        }
    }

    private ICommand _comandoExcluirNomeDescricao;
    public ICommand ComandoExcluirNomeDescricao
    {
        get
        {
            _comandoExcluirNomeDescricao ??= new RelayCommand<object>(param => Excluir(NomeDescricaoModel));
            return _comandoExcluirNomeDescricao;
        }
    }

    private ICommand _comandoAtualizarNomeDescricao;
    public ICommand ComandoAtualizarNomeDescricao
    {
        get
        {
            _comandoAtualizarNomeDescricao ??= new RelayCommand<object>(param => Atualizar());
            return _comandoAtualizarNomeDescricao;
        }
    }

    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickNomeDescricao;
    public ICommand ComandoDuploClickNomeDescricao
    {
        get
        {
            _comandoDuploClickNomeDescricao ??= new RelayCommand<object>(param => DuploClickNomeDescricao(param));
            return _comandoDuploClickNomeDescricao;
        }
    }
}
