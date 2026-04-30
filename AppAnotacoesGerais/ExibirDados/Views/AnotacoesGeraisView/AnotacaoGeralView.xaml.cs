using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.ExibirDados.ViewModels.TelaPrincipalVM;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;

namespace AppAnotacoesGerais.ExibirDados.Views.AnotacoesGeraisView;

public partial class AnotacaoGeralView : UserControl
{
    public TelaPrincipalViewModel TelaPrincipalViewModel { get; set; } = new();
    public AnotacaoGeralModel AnotacaoGeralModel { get; set; } = new();
    public AnotacaoGeralView()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            EditarAnotacaoGeralView editarAnotacaoGeralView = new(AnotacaoGeralModel, null);
            AnotacaoGeral anotacaoGeral = new();
            ObservableCollection<AnotacaoGeralModel> listaDeAnotacaoGeralModel = new ObservableCollection<AnotacaoGeralModel>();
            AnotacaoGeralModel.Id = Convert.ToInt32(TxtId.Text);
            bool retorno = AnotacaoGeralRepositorio.VerificarRegistros(AnotacaoGeralModel.Id);
            if (retorno)
            {
                AnotacaoGeralModel.Id = AnotacaoGeralModel.Id;
                var listaAnotacaoGeral = AnotacaoGeralRepositorio.ObterAnotacoesGeraisPorId(AnotacaoGeralModel.Id);

                foreach (var item in listaAnotacaoGeral)
                    listaDeAnotacaoGeralModel.Add(new AnotacaoGeralModel
                    {
                        Id = item.Id,
                        NomeCategoria = item.NomeCategoria,
                        NomeSubcategoria = item.NomeSubcategoria,
                        NomeDaDescricao = item.NomeDaDescricao,
                        Descricao = item.Descricao,
                        Data = item.Data
                    });

                if (listaDeAnotacaoGeralModel.Count > 0 && listaDeAnotacaoGeralModel[0].Id > 0)
                {
                    if (listaDeAnotacaoGeralModel.Count >= 0)
                    {
                        if (listaDeAnotacaoGeralModel[0].GetType() == typeof(AnotacaoGeralModel))
                        {
                            AnotacaoGeralModel = listaDeAnotacaoGeralModel[0];
                            editarAnotacaoGeralView.TxtId.Text = Convert.ToString(AnotacaoGeralModel.Id.ToString());
                            editarAnotacaoGeralView.CbxCategoria.Text = AnotacaoGeralModel.NomeCategoria;
                            editarAnotacaoGeralView.CbxSubcategoria.Text = AnotacaoGeralModel.NomeSubcategoria;
                            editarAnotacaoGeralView.CbxNomeDaDescricao.Text = AnotacaoGeralModel.NomeDaDescricao;
                            editarAnotacaoGeralView.TxtDescricao.Text = AnotacaoGeralModel.Descricao;
                            editarAnotacaoGeralView.DtpData.Text = AnotacaoGeralModel.Data.ToString();
                            // Atribui a view de edição ao ViewModel principal para que o ContentControl principal exiba a UserControl
                            var mainVm = Application.Current?.MainWindow?.DataContext as TelaPrincipalViewModel;
                            if (mainVm != null)
                            {
                                mainVm.SelecionarControleDeUsuario = new EditarAnotacaoGeralView(AnotacaoGeralModel, null);// editarAnotacaoGeralView;
                            }
                        }
                    }
                }
            }
            else
            {
                Mensagens.NomeDoMetodo = "VerificarRegistros";
                Mensagens.ErroObterId(AnotacaoGeralModel.Id, Mensagens.NomeDoMetodo);
            }
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "EditarAnotacaoGeralComando";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return;
        }
    }
}
