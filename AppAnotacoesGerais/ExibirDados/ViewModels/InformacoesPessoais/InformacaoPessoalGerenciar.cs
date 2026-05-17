using AppAnotacoesGerais.AcessarDados.Entidades;
using AppAnotacoesGerais.GerenciarDados;
using AppAnotacoesGerais.GerenciarDados.Repositorios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppAnotacoesGerais.ExibirDados.ViewModels.InformacoesPessoais;

public partial class InformacaoPessoalViewModel//InformacaoPessoalGerenciar
{
    /*
    public void ConsultasDeInformacoesPessoais()
    {
        try
        {
            var listaDeInformacoesPessoais = new ObservableCollection<InformacaoPessoal>();
            listaDeInformacoesPessoais = [.. InformacaoPessoalRepositorio.ObterInformacoesPessoais() ?? []];

            _listaDeInformacoesPessoais.Clear();
            foreach (var item in listaDeInformacoesPessoais)
                _listaDeInformacoesPessoais.Add(item);
        }
        catch (Exception ex)
        {
            Mensagens.NomeDoMetodo = "ConsultasDeInformacoesPessoais";
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
            return;
        }
    }*/
}
