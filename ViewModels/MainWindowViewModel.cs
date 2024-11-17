using Avalonia.ReactiveUI;
using Avalonia.Controls;
using System.Windows.Input;
using ReactiveUI;
using CommunityToolkit.Mvvm.Input;
using System;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia;
using System.Reactive;
using Dock.Views;

namespace Dock.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> OpenNewWindowCommand { get; }

        public ICommand ButtonClickCommand { get; }


    public MainWindowViewModel()
    {   
        OpenNewWindowCommand = ReactiveCommand.Create(OpenNewWindow);
        ButtonClickCommand = new RelayCommand(OnButtonClick);
    }
    private void OpenNewWindow()
        {
            var newWindow = new NewWindow();
            newWindow.Show();
        }
        private void OnButtonClick()
        {
            Environment.Exit(0);
        }

    }
    
    
}