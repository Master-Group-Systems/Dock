using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia;
using Avalonia.Media;
using Dock.ViewModels;

namespace Dock.Views;

public partial class MainWindow : Window
{
    
    private Point _PontoInicial;
    
    public MainWindow()
    {
        InitializeComponent();

        this.DataContext = new MainWindowViewModel();
        
        this.PointerPressed += PonteiroPrecionado;
        this.PointerMoved += PonteiroMovido;
        this.PointerReleased += PonteiroSolto;
    }

    private void PonteiroPrecionado(object sender, PointerPressedEventArgs e)
    {
        _PontoInicial = e.GetPosition(this);
    }

    private void PonteiroMovido(object sender, PointerEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            var position = e.GetPosition(this);
            var deltaX = position.X - _PontoInicial.X;
            var deltaY = position.Y - _PontoInicial.Y;

            Position = new PixelPoint((int)(Position.X + deltaX), (int)(Position.Y + deltaY));
        }
    }

    private void PonteiroSolto(object sender, PointerReleasedEventArgs e) { }
}