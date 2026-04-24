using System;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.Helpers;

public static class InputHelpers
{
    // Verifica se o parâmetro recebido representa a tecla Enter.
    public static bool IsEnterKey(object param)
    {
        Key? key = null;

        if (param is KeyEventArgs ke)
        {
            key = ke.Key;
        }
        else if (param is Key k)
        {
            key = k;
        }
        else if (param is string s && Enum.TryParse<Key>(s, out var ks))
        {
            key = ks;
        }

        return key == Key.Enter;
    }
}
