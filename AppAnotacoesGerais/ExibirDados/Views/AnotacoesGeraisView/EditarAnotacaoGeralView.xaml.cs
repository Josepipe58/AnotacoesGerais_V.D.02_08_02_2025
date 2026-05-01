using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.ExibirDados.ViewModels.AnotacoesGerais;
using System.Windows.Controls;

namespace AppAnotacoesGerais.ExibirDados.Views.AnotacoesGeraisView;

public partial class EditarAnotacaoGeralView : UserControl
{
    public AnotacaoGeralViewModel AnotacaoGeralViewModel { get; set; } = new();
    public CategoriaModel CategoriaModel { get; set; } = new();
    public SubcategoriaModel SubcategoriaModel { get; set; } = new();
    public AnotacaoGeralModel AnotacaoGeralModel { get; set; } = new();
    public EditarAnotacaoGeralView(AnotacaoGeralModel model, AnotacaoGeralViewModel origemViewModel)
    {
        InitializeComponent();

        // Use o ViewModel de origem se fornecido, caso contrário crie um novo para evitar NullReference
        var vm = origemViewModel ?? new AnotacaoGeralViewModel();

        // Se houver um viewmodel de origem, reutilize coleções existentes (com checagem de null)
        if (origemViewModel != null && model != null)
        {
            if (origemViewModel.CategoriaModel?.ListaDeCategorias != null)
                vm.CategoriaModel.ListaDeCategorias = origemViewModel.CategoriaModel.ListaDeCategorias;

            if (origemViewModel.SubcategoriaModel?.ListaDeSubcategorias != null)
                vm.SubcategoriaModel.ListaDeSubcategorias = origemViewModel.SubcategoriaModel.ListaDeSubcategorias;

            if (origemViewModel.NomeDescricaoModel.ListaDoNomeDescricao != null)
                vm.NomeDescricaoModel.ListaDoNomeDescricao = origemViewModel.NomeDescricaoModel.ListaDoNomeDescricao;
        }

        // Atribuir o modelo a ser editado (usar novo se for nulo)
        vm.AnotacaoGeralModel = model ?? new AnotacaoGeralModel();

        // Ajustar seleções dos ComboBoxes com base nos nomes armazenados no modelo (checagens de null)
        if (vm.CategoriaModel?.ListaDeCategorias != null && model != null)
        {
            vm.CategoriaSelecionada = vm.CategoriaModel.ListaDeCategorias
                .FirstOrDefault(c => c.NomeCategoria == model.NomeCategoria);
        }

        if (vm.SubcategoriaModel?.ListaDeSubcategorias != null && model != null)
        {
            vm.SubcategoriaSelecionada = vm.SubcategoriaModel.ListaDeSubcategorias
                .FirstOrDefault(sc => sc.NomeSubcategoria == model.NomeSubcategoria);
        }

        if (vm.NomeDescricaoModel?.ListaDoNomeDescricao != null && model != null)
        {
            vm.NomeDescricaoSelecionada = vm.NomeDescricaoModel.ListaDoNomeDescricao
                .FirstOrDefault(nd => nd.NomeDaDescricao == model.NomeDaDescricao);
        }

        // Expor e associar o ViewModel à View
        this.AnotacaoGeralViewModel = vm;
        this.DataContext = vm;
    }
}
