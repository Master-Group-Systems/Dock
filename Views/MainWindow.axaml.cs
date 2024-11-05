using System;
using Avalonia.Controls;
using Avalonia.Input;
using Dock.ViewModels;

namespace Dock.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.DataContext = new MainWindowViewModel();
    
        this.PointerPressed += OnPointerPressed;
    }

    private void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            // Cria o objeto com os parâmetros e executa o comando
            var moveParams = new MoveWindowParams(this, e);
            viewModel.MoveWindowCommand.Execute(moveParams);
        }
    }
}