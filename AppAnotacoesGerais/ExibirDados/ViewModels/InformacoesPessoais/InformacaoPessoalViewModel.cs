using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.ExibirDados.Models;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System.Collections.ObjectModel;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.InformacoesPessoais;

public partial class InformacaoPessoalViewModel
{
    public CategoriaRepositorio _categoriaRepositorio = new();
    public SubcategoriaRepositorio _subcategoriaRepositorio = new();
    public NomeDescricaoRepositorio _nomeDescricaoRepositorio = new();
    public InformacaoPessoalRepositorio _informacaoPessoalRepositorio = new();

    public InformacaoPessoalModel InformacaoPessoalModel { get; set; } = new();
    public CategoriaModel CategoriaModel { get; set; } = new();
    public SubcategoriaModel SubcategoriaModel { get; set; } = new();
    public NomeDescricaoModel NomeDescricaoModel { get; set; } = new();

    private readonly ObservableCollection<InformacaoPessoal> _listaDeInformacoesPessoais = [];
    public ReadOnlyObservableCollection<InformacaoPessoal> ListaDeInformacoesPessoais { get; }

    public InformacaoPessoalViewModel()
    {
        //Carregar DataGrid de Anotações Gerais.
        //Usando encapsulamento para obter a lista de Anotações Gerais do repositório e armazená-la em uma coleção observável.
        ListaDeInformacoesPessoais = new ReadOnlyObservableCollection<InformacaoPessoal>(_listaDeInformacoesPessoais);
        ObterListaDeInformacoesPessoais();
    }

    public void ObterListaDeInformacoesPessoais()
    {
        var listaDeInformacoesPessoais = new ObservableCollection<InformacaoPessoal>();
        listaDeInformacoesPessoais = [.. InformacaoPessoalRepositorio.ObterInformacoesPessoais() ?? []];

        _listaDeInformacoesPessoais.Clear();
        foreach (var item in listaDeInformacoesPessoais)
            _listaDeInformacoesPessoais.Add(item);
    }

    public void AtualizarInformacaoPessoal()
    {
        InformacaoPessoalModel.Id = 0;
        ObterListaDeInformacoesPessoais();
    }

    public void LimparAdicionarEditar()
    {
        InformacaoPessoalModel.Id = 0;
        InformacaoPessoalModel.Titulo = null;
        InformacaoPessoalModel.Descricao = null;
    }
}
