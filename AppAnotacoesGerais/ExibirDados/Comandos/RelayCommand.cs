using AppAnotacoesGerais.GerenciarDados;
using System;
using System.Windows.Input;

namespace AppAnotacoesGerais.ExibirDados.Comandos;

public sealed class RelayCommand<T> : ICommand
{
    private readonly Action<T> _execute;
    private readonly Func<T, bool> _canExecute;

    public RelayCommand(Action<T> execute) : this(execute, null) { }

    public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter)
    {
        if (_canExecute == null)
            return true;

        if (parameter is T t)
            return _canExecute(t);

        if (parameter == null)
        {
            // null é aceitável para tipos de referência
            if (default(T) == null)
                return _canExecute(default!);
            // Para tipos de valor, use default(T)
            return _canExecute(default!);
        }

        try
        {
            var converted = (T)Convert.ChangeType(parameter, typeof(T));
            return _canExecute(converted);
        }
        catch
        {
            return false;
        }
    }

    public void Execute(object parameter)
    {
        if (parameter is T t)
        {
            _execute(t);
            return;
        }

        if (parameter == null)
        {
            _execute(default!);
            return;
        }

        try
        {
            var converted = (T)Convert.ChangeType(parameter, typeof(T));
            _execute(converted);
        }
        catch(Exception ex) 
        {
            Mensagens.NomeDoMetodo = nameof(Execute);
            Mensagens.ErroDeExcecaoENomeDoMetodo(ex, Mensagens.NomeDoMetodo);
        }
    }
}
