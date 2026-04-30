using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;
using System.Windows;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.AnotacoesGerais;

public partial class AnotacaoGeralViewModel// AnotacaoGeralGerenciar
{
    public void ConsultasDeAnotacoesGerais()
    {
        try
        {
            var listaDeAnotacoesGerais = new ObservableCollection<AnotacaoGeral>();
            if (string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeCategoria) && string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeSubcategoria)
                && string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeDaDescricao))
            {
                listaDeAnotacoesGerais = [.. AnotacaoGeralRepositorio.ObterAnotacoesGerais() ?? []];
                    
                _listaDeAnotacoesGerais.Clear();
                foreach (var item in listaDeAnotacoesGerais)
                    _listaDeAnotacoesGerais.Add(item);
            }
            else if (!string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeCategoria) && string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeSubcategoria)
                && string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeDaDescricao))
            {
                listaDeAnotacoesGerais = [.. AnotacaoGeralRepositorio.ObterAnotacoesGerais().Where(x => x.NomeCategoria == AnotacaoGeralModel.NomeCategoria)
                   ?? new ObservableCollection<AnotacaoGeral>()];

                _listaDeAnotacoesGerais.Clear();
                foreach (var item in listaDeAnotacoesGerais)
                    _listaDeAnotacoesGerais.Add(item);                
            }
            else if (!string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeCategoria) && !string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeSubcategoria)
                && string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeDaDescricao))
            {
                listaDeAnotacoesGerais = [.. AnotacaoGeralRepositorio.ObterAnotacoesGerais().Where(dp => dp.NomeCategoria == AnotacaoGeralModel.NomeCategoria
                && dp.NomeSubcategoria == AnotacaoGeralModel.NomeSubcategoria)];

                _listaDeAnotacoesGerais.Clear();
                foreach (var item in listaDeAnotacoesGerais)
                    _listaDeAnotacoesGerais.Add(item);
            }
            else if (!string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeCategoria) && !string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeSubcategoria)
                && !string.IsNullOrWhiteSpace(AnotacaoGeralModel.NomeDaDescricao))
            {
                listaDeAnotacoesGerais = [.. AnotacaoGeralRepositorio.ObterAnotacoesGerais().Where(dp => dp.NomeCategoria == AnotacaoGeralModel.NomeCategoria
                && dp.NomeSubcategoria == AnotacaoGeralModel.NomeSubcategoria && dp.NomeDaDescricao == AnotacaoGeralModel.NomeDaDescricao)];

                _listaDeAnotacoesGerais.Clear();
                foreach (var item in listaDeAnotacoesGerais)
                    _listaDeAnotacoesGerais.Add(item);
            }
            else
            {
                MessageBox.Show("Não foi possível fazer nenhum tipo de consulta nas anotações gerais.", "Mensagem de Erro!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ConsultasDeDespesas";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return;
        }
    }

    public void LimparDados()
    {
        AnotacaoGeralModel.Id = 0;
        AnotacaoGeralModel.NomeCategoria = null;
        AnotacaoGeralModel.NomeSubcategoria = null;
        AnotacaoGeralModel.NomeDaDescricao = null;
        
        //Carregar DataGrid de Anotações Gerais.
        var listaDeAnotacoesGerais = new ObservableCollection<AnotacaoGeral>();
        listaDeAnotacoesGerais = [.. AnotacaoGeralRepositorio.ObterAnotacoesGerais() ?? []];

        _listaDeAnotacoesGerais.Clear();
        foreach (var item in listaDeAnotacoesGerais)
            _listaDeAnotacoesGerais.Add(item);

        //var listaDeCategorias = _categoriaRepositorio.ObterListaDeTodos().ToList() ?? [];
        //AnotacaoGeralModel.ListaDeAnotacoesGerais = new ObservableCollection<Categoria>(listaDeCategorias);
    }
}
