using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.ExibirDados.ViewModels.AnotacoesGerais;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Windows;

namespace AppAnotacoesGerais.ExibirDados.Views.AnotacoesGeraisView;

public partial class EditarAnotacaoGeralWindow : Window
{
    public EditarAnotacaoGeralWindow(AnotacaoGeralModel model, AnotacaoGeralViewModel origemViewModel)
    {
        InitializeComponent();

        // Criar um ViewModel para a view de edição e inicializar listas/seleções
        var vm = new AnotacaoGeralViewModel();

        // Reutilizar as listas do viewmodel de origem (evita consultas adicionais)
        if (origemViewModel != null)
        {
            vm.CategoriaModel.ListaDeCategorias = origemViewModel.CategoriaModel.ListaDeCategorias;
            vm.SubcategoriaModel.ListaDeSubcategorias = origemViewModel.SubcategoriaModel.ListaDeSubcategorias;
            vm.ListaDoNomeDescricao = origemViewModel.ListaDoNomeDescricao;
        }

        // Atribuir o modelo a ser editado (já é uma cópia criada pelo comando)
        vm.AnotacaoGeralModel = model;

        // Ajustar seleções dos ComboBoxes com base nos nomes armazenados no modelo
        if (vm.CategoriaModel.ListaDeCategorias != null)
        {
            vm.CategoriaSelecionada = vm.CategoriaModel.ListaDeCategorias
                .FirstOrDefault(c => c.NomeCategoria == model.NomeCategoria);
        }

        if (vm.SubcategoriaModel.ListaDeSubcategorias != null)
        {
            vm.SubcategoriaSelecionada = vm.SubcategoriaModel.ListaDeSubcategorias
                .FirstOrDefault(sc => sc.NomeSubcategoria == model.NomeSubcategoria);
            vm.SubcategoriaModel.IndiceSelecionadoSubcategoria = 0; // Forçar atualização da lista de Subcategoria
                                                                    // ao selecionar Subcategoria
        }

        if (vm.ListaDoNomeDescricao != null)
        {
            vm.NomeDescricaoSelecionada = vm.ListaDoNomeDescricao
                .FirstOrDefault(nd => nd.NomeDaDescricao == model.NomeDaDescricao);
            vm.IndiceSelecionadoNomeDescricao = 0; // Forçar atualização da lista de NomeDescricao ao selecionar Subcategoria
        }

        // Criar a view e definir seu DataContext para o vm preparado
        var view = new EditarAnotacaoGeralExcluir();
        view.DataContext = vm;
        ContentHost.Content = view;
    }

    private void SalvarEFechar(bool salvar)
    {
        // Definir DialogResult para sinalizar sucesso
        this.DialogResult = salvar;
        this.Close();
    }

    private void BtnSalvar_Click(object sender, RoutedEventArgs e)
    {
        // Obter o ViewModel ligado à view interna e persistir alterações.
        if (ContentHost.Content is EditarAnotacaoGeralExcluir view && view.DataContext is AnotacaoGeralViewModel vm)
        {
            try
            {
                // Criar entidade a partir do model e chamar repositório para editar.
                var model = vm.AnotacaoGeralModel;
                var entidade = new AcessarDados.Entidades.AnotacaoGeral
                {
                    Id = model.Id,
                    NomeCategoria = model.NomeCategoria,
                    NomeSubcategoria = model.NomeSubcategoria,
                    NomeDaDescricao = model.NomeDaDescricao,
                    Descricao = model.Descricao,
                    Data = model.Data
                };

                using var repo = new AnotacaoGeralRepositorio();
                repo.Editar(entidade);
                Mensagens.SucessoAoEditar(entidade.Id);
                SalvarEFechar(true);
            }
            catch (Exception ex)
            {
                Mensagens.NomeDoMetodo = "SalvarEdicao";
                Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            }
        }
    }

    private void BtnCancelar_Click(object sender, RoutedEventArgs e)
    {
        SalvarEFechar(false);
    }
}