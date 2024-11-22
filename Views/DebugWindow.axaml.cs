using System;
using System.IO;
using Avalonia.Media.Imaging;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia;
using Avalonia.Media;
using Dock.ViewModels;
using Avalonia.Interactivity;
using Dock.Models;
using DynamicData;

namespace Dock.Views;

public partial class DebugWindow : Window
{
    
    private TextBox TextX;
    private static PathDir P = new PathDir();
    private string conexao = "Data Source="+$"{P.GetPastaDoUsuario()}"+"\\meu_banco_de_dados.db";
    
    public DebugWindow()
    {
        InitializeComponent();
        
        this.DataContext = new DebugWindowViewModel();
 
        Button btnTESTE = new()
        {
            Content = "TESTE",
            Width = 100,
            FontSize = 14,
            Height = 45,
            Foreground = Brushes.Black,
            BorderBrush= Brushes.White,
            CornerRadius=new CornerRadius(40),
            BorderThickness = new Avalonia.Thickness(),
            Margin = new Avalonia.Thickness(10),
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Background = Brushes.LawnGreen
        };
        Button btnLIMPAR = new()
        {
            Content = "LIMPAR",
            Width = 100,
            FontSize = 14,
            Height = 45,
            Foreground = Brushes.Black,
            BorderBrush= Brushes.White,
            CornerRadius=new CornerRadius(40),
            BorderThickness = new Avalonia.Thickness(),
            Margin = new Avalonia.Thickness(10),
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Background = Brushes.LawnGreen
        };
        
        TextX = new()
        {
            TextWrapping = Avalonia.Media.TextWrapping.Wrap,
            AcceptsReturn = true,
            Width = 300,
            Height = 300
        };


        btnTESTE.Click += btnTESTE_Click;
        btnLIMPAR.Click += btnLIMPAR_Click;

        TextX.Text = $"HI\nhi\n";
        StakBTN.Children.Add(btnTESTE);
        StakBTN.Children.Add(btnLIMPAR);
        StakBase.Children.Add(TextX);
    }

    private void btnTESTE_Click(object sender, RoutedEventArgs e)
    {
        TextX.Text = $"{TextX.Text}"+$"{P.GetPastaDoUsuario()}\n";
    }
    
    private void btnLIMPAR_Click(object sender, RoutedEventArgs e)
    {
        TextX.Text = "";
    }
    
}