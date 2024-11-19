using System;
using System.IO;
using Avalonia.Media.Imaging;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia;
using Avalonia.Media;
using Dock.ViewModels;
using Dock.Models;
using Avalonia.Interactivity;

namespace Dock.Views;

public partial class MainWindow : Window
{
    
    private Point _pontoinicial;
    
    
    public MainWindow()
    {
        InitializeComponent();
        
        var debugWindow = new DebugWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };
        debugWindow.Show();

        this.DataContext = new MainWindowViewModel();
        this.PointerPressed += PonteiroPrecionado;
        this.PointerMoved += PonteiroMovido;
        this.PointerReleased += PonteiroSolto;
        
        Button CLOSE = new()
        {
            Content = null,
            Width = 50,
            Height = 50,
            BorderBrush= Brushes.White,
            CornerRadius=new CornerRadius(40),
            BorderThickness = new Thickness(0),
            Margin = new Thickness(0),
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Background = new ImageBrush(new Bitmap("Assets/icon-close.png"))
        };
        
        Button CHROME = new()
        {
            Content = null,
            Width = 50,
            Height = 50,
            BorderBrush= Brushes.White,
            CornerRadius=new CornerRadius(40),
            BorderThickness = new Thickness(0),
            Margin = new Thickness(0),
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Background = new ImageBrush(new Bitmap("Assets/cromada.png"))
        };
        
        Button DISCORD = new()
        {
            Content = null,
            Width = 50,
            Height = 50,
            BorderBrush= Brushes.White,
            CornerRadius=new CornerRadius(40),
            BorderThickness = new Thickness(0),
            Margin = new Thickness(0),
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Background = new ImageBrush(new Bitmap("Assets/discordia.png"))
        };
        
        Button FACEBOOK = new()
        {
            Content = null,
            Width = 50,
            Height = 50,
            BorderBrush= Brushes.White,
            CornerRadius=new CornerRadius(40),
            BorderThickness = new Thickness(0),
            Margin = new Thickness(0),
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Background = new ImageBrush(new Bitmap("Assets/spotify.png"))
        };
        
        Button SPOTIFY = new()
        {
            Content = null,
            Width = 50,
            Height = 50,
            BorderBrush= Brushes.White,
            CornerRadius=new CornerRadius(40),
            BorderThickness = new Thickness(0),
            Margin = new Thickness(0),
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Background = new ImageBrush(new Bitmap("Assets/icon-close.png"))
        };
        
        Button CONFIG = new()
        {
            Content = null,
            Width = 50,
            Height = 50,
            BorderBrush= Brushes.White,
            CornerRadius=new CornerRadius(40),
            BorderThickness = new Thickness(0),
            Margin = new Thickness(0),
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Background = new ImageBrush(new Bitmap("Assets/icons-settings.png"))
        };
        
        StakBase.Children.Add(CLOSE);
        StakBase.Children.Add(CHROME);
        StakBase.Children.Add(DISCORD);
        StakBase.Children.Add(FACEBOOK);
        StakBase.Children.Add(SPOTIFY);
        StakBase.Children.Add(CONFIG);
    }

    private void PonteiroPrecionado(object sender, PointerPressedEventArgs e)
    {
        if (e.Source is not Button)
        
        {
            _pontoinicial = e.GetPosition(this);
        }
    }

    private void PonteiroMovido(object sender, PointerEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed && e.Source is not Button)
        
        {
            var position = e.GetPosition(this);
            var deltaX = position.X - _pontoinicial.X;
            var deltaY = position.Y - _pontoinicial.Y;
            Position = new PixelPoint((int)(Position.X + deltaX), (int)(Position.Y + deltaY));
        }
    }

    private void PonteiroSolto(object sender, PointerReleasedEventArgs e) { }
    
}