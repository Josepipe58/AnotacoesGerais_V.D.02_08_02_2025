using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Comandos;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Windows;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.AnotacoesGerais;

public partial class AnotacaoGeralViewModel// AnotacaoGeralComandos
{
    private ICommand _comandoExcluirAnotacaoGeral;
    public ICommand ComandoExcluirAnotacaoGeral
    {
        get
        {
            _comandoExcluirAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                // Criar uma nova instância de AnotacaoGeralModel para evitar
                // modificar a instância vinculada à interface do usuário.
                AnotacaoGeralModel anotacaoGeralModel = new();
                anotacaoGeralModel = AnotacaoGeralModel;

                if (anotacaoGeralModel.Id > 0)
                {
                    MessageBoxResult resultado = Mensagens.ConfirmarExcluir(anotacaoGeralModel.Id);
                    if (resultado == MessageBoxResult.No)
                    {
                        AnotacaoGeralModel.Id = 0;                     
                        return;
                    }
                    try
                    {
                        AnotacaoGeralRepositorio anotacaoGeralRepositorio = new();
                        AnotacaoGeral anotacaoGeral = new()
                        {
                            Id = anotacaoGeralModel.Id
                        };
                        anotacaoGeralRepositorio.Excluir(anotacaoGeral);
                        Mensagens.SucessoAoExcluir(anotacaoGeral.Id);
                    }
                    catch (Exception erro)
                    {
                        Mensagens.NomeDoMetodo = "Excluir";
                        Mensagens.ErroDeExcecaoENomeDoMetodo(erro, Mensagens.NomeDoMetodo);
                        return;
                    }
                }               
                else
                {
                    Mensagens.PreencherCampoVazio();
                    return;
                }
            });
            return _comandoExcluirAnotacaoGeral;
        }
    }

    private ICommand _comandoAtualizarAnotacaoGeral;
    public ICommand ComandoAtualizarAnotacaoGeral
    {
        get
        {
            _comandoAtualizarAnotacaoGeral ??= new RelayCommand<object>(param =>
            {
                AnotacaoGeralModel.Id = 0;
                AnotacaoGeralModel.NomeCategoria = "";
                AnotacaoGeralModel.NomeSubcategoria = "";
                AnotacaoGeralModel.NomeDaDescricao = "";
                ConsultasDeAnotacoesGerais();
            });
            return _comandoAtualizarAnotacaoGeral;
        }
    }

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
                    AnotacaoGeralModel.Id = anotacaoGeral.Id;   
                }
            });
            return _comandoDuploClickAnotacaoGeral;
        }
    }
}
