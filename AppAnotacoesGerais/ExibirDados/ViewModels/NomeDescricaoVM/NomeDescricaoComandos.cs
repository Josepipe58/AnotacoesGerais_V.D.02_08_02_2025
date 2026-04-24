using AppAnotacoesGerais.ExibirDados.Comandos;
using System;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.NomeDescricaoVM;

public partial class NomeDescricaoViewModel//NomeDescricaoComandos
{
    private ICommand _comandoAdicionarNomeDaDescricao;
    public ICommand ComandoAdicionarNomeDaDescricao
    {
        get
        {
            _comandoAdicionarNomeDaDescricao ??= new RelayCommand<object>(param => Adicionar(NomeDaDescricaoModel));
            return _comandoAdicionarNomeDaDescricao;
        }
    }

    private ICommand _comandoEditarNomeDaDescricao;
    public ICommand ComandoEditarNomeDaDescricao
    {
        get
        {
            _comandoEditarNomeDaDescricao ??= new RelayCommand<object>(param => Editar(NomeDaDescricaoModel));
            return _comandoEditarNomeDaDescricao;
        }
    }

    private ICommand _comandoExcluirNomeDaDescricao;
    public ICommand ComandoExcluirNomeDaDescricao
    {
        get
        {
            _comandoExcluirNomeDaDescricao ??= new RelayCommand<object>(param => Excluir(NomeDaDescricaoModel));
            return _comandoExcluirNomeDaDescricao;
        }
    }

    private ICommand _comandoAtualizarNomeDaDescricao;
    public ICommand ComandoAtualizarNomeDaDescricao
    {
        get
        {
            _comandoAtualizarNomeDaDescricao ??= new RelayCommand<object>(param => Atualizar());
            return _comandoAtualizarNomeDaDescricao;
        }
    }

    //Comando do evento MouseDoubleClick.
    private ICommand _comandoDuploClickNomeDaDescricao;
    public ICommand ComandoDuploClickNomeDaDescricao
    {
        get
        {
            _comandoDuploClickNomeDaDescricao ??= new RelayCommand<object>(param => DuploClickNomeDaDescricao(param));
            return _comandoDuploClickNomeDaDescricao;
        }
    }
}
