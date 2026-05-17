using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.ExibirDados.ViewModels.InformacoesPessoais;
using System.Windows.Controls;

namespace AppAnotacoesGerais.ExibirDados.Views.InformacoesPessoaisView
{
    public partial class InformacaoPessoalGerenciarView : UserControl
    {
        public InformacaoPessoalGerenciarView()
        {
            InitializeComponent();
        }

        public InformacaoPessoalGerenciarView(InformacaoPessoalModel model, InformacaoPessoalViewModel origemViewModel)
        {
            InitializeComponent();

            // Use o ViewModel de origem se fornecido, caso contrário crie um novo para evitar NullReference
            var vm = origemViewModel ?? new InformacaoPessoalViewModel();

            // Atribuir o modelo a ser editado (usar novo se for nulo)
            vm.InformacaoPessoalModel = model ?? new InformacaoPessoalModel();

            //Não deletar esse DataContext, ele é necessário para que os bindings funcionem corretamente.
            //Ele deve ser o último comando do construtor para garantir que o ViewModel esteja totalmente
            //configurado antes de ser atribuído à view.
            this.DataContext = vm;
        }
    }
}
