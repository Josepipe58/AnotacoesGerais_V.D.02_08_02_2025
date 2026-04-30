using System.Linq;
using System.Windows;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.ExibirDados.ViewModels.AnotacoesGerais;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.AcessarDados.Entidades;

namespace AppAnotacoesGerais.ExibirDados.Views.AnotacaoGeral;

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
        }

        if (vm.ListaDoNomeDescricao != null)
        {
            vm.NomeDescricaoSelecionada = vm.ListaDoNomeDescricao
                .FirstOrDefault(nd => nd.NomeDaDescricao == model.NomeDaDescricao);
        }

        // Criar a view e definir seu DataContext para o vm preparado
        var view = new EditarAnotacaoGeralView();
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
        if (ContentHost.Content is EditarAnotacaoGeralView view && view.DataContext is ViewModels.AnotacoesGerais.AnotacaoGeralViewModel vm)
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

                using var repo = new GerenciarDados.Repositorios.AnotacaoGeralRepositorio();
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

    // Possíveis causas do CS0103 para InitializeComponent:
    // - O x:Class no XAML não coincide com este namespace+nome de classe.
    // - O arquivo XAML não está marcado como "Page" (Build Action).
    // - Problemas no projeto que impedem a geração do arquivo *.g.cs.
    // Como correção imediata e para permitir a compilação enquanto investiga:
    [System.Diagnostics.DebuggerNonUserCode]
    private void InitializeComponent()
    {
        // Tenta carregar o XAML embutido—funciona quando o recurso XAML está corretamente
        // incluído no assembly. Se o XAML não estiver sendo gerado/embutido, verifique:
        // 1) o x:Class do arquivo .xaml combina com este namespace e nome de classe;
        // 2) o Build Action do .xaml está como "Page";
        // 3) o projeto é do tipo WPF e o sistema de build está ativo.
        var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        var uri = new System.Uri("/" + assemblyName + ";component/ExibirDados/Views/AnotacaoGeral/EditarAnotacaoGeralWindow.xaml", System.UriKind.Relative);
        System.Windows.Application.LoadComponent(this, uri);
    }
}