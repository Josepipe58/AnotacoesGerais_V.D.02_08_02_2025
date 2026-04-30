using System.Windows.Controls;
using AppAnotacoesGerais.ExibirDados.Models;

namespace AppAnotacoesGerais.ExibirDados.Views.AnotacoesGeraisView
{
    public partial class EditarAnotacaoGeralExcluir : UserControl
    {
        //public AcessarDados.Entidades.AnotacaoGeral AnotacaoGeral { get; set; } = new();
        public EditarAnotacaoGeralExcluir()
        {
            InitializeComponent();
        }

        // Construtor adicional para aceitar um modelo ao criar a view.
        public EditarAnotacaoGeralExcluir(AnotacaoGeralModel anotacaoGeralModel) : this()
        {
            if (anotacaoGeralModel == null) return;
            // Criar um ViewModel local para a view de edição e atribuir o modelo recebido.
            // Assim as bindings XAML que referenciam AnotacaoGeralModel.* funcionarão corretamente.
            DataContext = new ViewModels.AnotacoesGerais.AnotacaoGeralViewModel
            {
                AnotacaoGeralModel = anotacaoGeralModel                
            };
        }
    }
}
