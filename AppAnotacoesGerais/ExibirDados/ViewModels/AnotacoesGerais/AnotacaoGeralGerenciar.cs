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
                return;
            }
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ConsultasDeAnotacoesGerais";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return;
        }
    }

    public void ContadorDeRegistros()
    {
        int contador = _anotacaoGeralRepositorio.ContadorRegistros();
        if (contador <= 0)
        {
            MessageBox.Show($"Atenção! Não existe nenhum registro no Banco de Dados.",
                        "Aviso!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        AnotacaoGeralModel.ContadorRegistros = contador;
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
    }

    public void AtualizarAnotacaoGeralGerenciarView()
    {
        AnotacaoGeralModel.Id = 0;        
        AnotacaoGeralModel.NomeCategoria = CategoriaModel.ListaDeCategorias[0].NomeCategoria;
        AnotacaoGeralModel.NomeSubcategoria = SubcategoriaModel.ListaDeSubcategorias[0].NomeSubcategoria;
        AnotacaoGeralModel.NomeDaDescricao = NomeDescricaoModel.ListaDoNomeDescricao[0].NomeDaDescricao;
        AnotacaoGeralModel.Descricao = null;
        AnotacaoGeralModel.Data = DateTime.Now;
        TextoPesquisa = string.Empty;
    }

    public void LimparAnotacaoGeralGerenciarView()
    {
        AnotacaoGeralModel.Id = 0;
        AnotacaoGeralModel.Descricao = null;
        AnotacaoGeralModel.Data = DateTime.Now;
    }
}
