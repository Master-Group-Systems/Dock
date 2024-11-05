using DockControlwindow;
using Avalonia.ReactiveUI;
using Avalonia.Controls;
using System.Windows.Input;
using ReactiveUI;
using System;
using Avalonia.Input;

namespace Dock.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public static string Greeting => "Welcome to Avalonia!";
         private readonly Controlwindow _controlWindow;

        public ICommand MoveWindowCommand { get; }

        public MainWindowViewModel()
        {
            _controlWindow = new Controlwindow();
            MoveWindowCommand = ReactiveCommand.Create<MoveWindowParams>(ExecuteMoveWindowCommand);
        }

        private void ExecuteMoveWindowCommand(MoveWindowParams parameters)
        {
            if (parameters?.Window != null && parameters.EventArgs != null)
            {
                _controlWindow.MoveWindow(parameters.Window, parameters.EventArgs);
            }
        }
    }

    public class MoveWindowParams
    {
        public Window Window { get; }
        public PointerPressedEventArgs EventArgs { get; }

        public MoveWindowParams(Window window, PointerPressedEventArgs eventArgs)
        {
            Window = window;
            EventArgs = eventArgs;
        }
    }
}