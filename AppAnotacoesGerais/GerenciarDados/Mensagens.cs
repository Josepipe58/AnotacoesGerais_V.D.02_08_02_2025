using System.Windows;

namespace AppAnotacoesGerais.GerenciarDados;

public static class Mensagens
{
    public static string NomeDoMetodo { get; set; } = string.Empty;

    //Mensagens de CRUD com Sucesso
    public static void SucessoAoAdicionar(int id)
    {
        MessageBox.Show($"Registro adicionado com sucesso.\nCódigo do registro: {id}",
                   "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static void SucessoAoEditar(int id)
    {
        MessageBox.Show($"Registro alterado com sucesso.\nCódigo do registro: {id}",
                     "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static void SucessoAoExcluir(int id)
    {
        MessageBox.Show($"Registro excluído com sucesso.\nCódigo do registro: {id}",
                   "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static MessageBoxResult ConfirmarExcluir(int id)
    {
        return MessageBox.Show($"Você tem certeza que deseja excluir esse registro?" +
        $"\nNúmero do Código: {id}", "Cuidado! Atenção!",
        MessageBoxButton.YesNo, MessageBoxImage.Information);
    }

    //Mensagens de CRUD com Erros
    public static void ErroAoAdicionar()
    {
        MessageBox.Show("Atenção!\nO campo Id tem que ser igual a 0 ou vazio.\nOutra opção é clicar no botão Editar ou Excluir." +
                "\nCorrija esses erros, para continuar.", "Atenção!", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static void ErroAoEditarOuExcluir()
    {
        MessageBox.Show("Atenção!\nO campo Id tem que ser maior do que 0.\nOutra opção é clicar no botão Adicionar." +
                "\nCorrija esses erros, para continuar.", "Atenção!", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static void PreencherCampoVazio()
    {
        MessageBox.Show("Atenção! Existe um ou mais de um campo, que está vazio.\n Corrija esse erro para continuar.",
            "Atenção!", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static void ErroDObterId(int id)
    {
        MessageBox.Show($"Não foi possível encontrar o {id},tente outro id diferente.\nCorrija esses erros, para continuar.",
            "Atenção!", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    //Erros do Try Catch(Exception)
    public static string ErroDeExcecaoENomeDoMetodo(Exception ex, string _nomeDoMetodo)
    {
        return MessageBox.Show($"Atenção! Ocorreu um erro no seguinte método: {_nomeDoMetodo}." +
                $"\nDetalhes: {ex.Message}", "Mensagem de Erro!", MessageBoxButton.OK, MessageBoxImage.Error).ToString();
    }

    //Mensagem de Erro de Switch Case
    public static void MensagemDeErroDeSwitchCase()
    {
        MessageBox.Show($"Atenção! Ocorreu um erro no Switch Case.\nNão foi possível selecionar um servidor.",
                        "Mensagem de Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    //Mensagem de Erro de Senha
    public static void ErroDeSenha()
    {
        MessageBox.Show($"Senha Incorreta!\nTente novamente.",
                        "Mensagem de Erro de Senha!", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
