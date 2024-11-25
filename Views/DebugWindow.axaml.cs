using System;
using System.IO;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia;
using Avalonia.Media;
using Dock.ViewModels;
using Avalonia.Interactivity;
using Dock.Models;
using System.Diagnostics;
using DynamicData;
using ReactiveUI;

namespace Dock.Views;

public partial class DebugWindow : Window
{
    
    private TextBox TextX;
    private static PathDir P = new PathDir();
    private static Atalho A = new Atalho();
    private static Data D = new Data();
    
    private string Conexao = "Data Source="+$"{P.GetPastaDoUsuario()}"+"\\meu_banco_de_dados.db";
    
    
    public DebugWindow()
    {
        InitializeComponent();
       
        this.DataContext = new DebugWindowViewModel();
        
        //Criar Banco de dados;
        D.CriarCanco(Conexao);

        //A.InserirDadosDeTeste(Conexao);
        
        var botoes = A.CriarBotoes(A.CarregarAtalhos(Conexao), StakBTN);

        // Adicionar os botões ao layout principal (usando StackPanel como exemplo)
        
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
            Background = Brushes.LawnGreen,
            Tag = new Dictionary<string, string>
            {
                { "CaminhoDoPrograma", "C:\\Windows\\system32\\notepad.exe" }, { "IconePng", "Assets/cromada.png" }
            }
        };
        
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
            Background = Brushes.LawnGreen,
            Tag = new Dictionary<string, string>
            {
                { "CaminhoDoPrograma", "C:\\Windows\\system32\\notepad.exe" }, { "IconePng", "Assets/discordia.png" }
            }
        };
        
        Button btnCHECAR = new()
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
            Background = Brushes.LawnGreen,
            Tag = new Dictionary<string, string>
            {
                { "CaminhoDoPrograma", "C:\\Windows\\system32\\notepad.exe" }, { "IconePng", "Assets/spotify.png" }
            }
        };
        
        var contextMenu = new ContextMenu();
        var editMenuItem = new MenuItem { Header = "Editar" };
        //editMenuItem.Click += (s, e) => AbrirJanelaDeEdicao(btnCHECAR);
        
        contextMenu.Items.Add(editMenuItem);
        btnCHECAR.ContextMenu = contextMenu;

        
        TextX = new()
        {
            TextWrapping = Avalonia.Media.TextWrapping.Wrap,
            AcceptsReturn = true,
            Width = 300,
            Height = 300
        };

        btnCHECAR.Click += btnCHECAR_Click;
        btnTESTE.Click += btnTESTE_Click;
        btnLIMPAR.Click += btnLIMPAR_Click;

        TextX.Text = $"HI\nhi\n";
        StakBTN.Children.Add(btnTESTE);
        StakBTN.Children.Add(btnCHECAR);
        StakBTN.Children.Add(btnLIMPAR);
        
        foreach (var botao in botoes)
        {
            StakBTN.Children.Add(botao);
        }
        
        StakBase.Children.Add(TextX);

    }
    /*
    private void AbrirJanelaDeEdicao(Button button)
    { 
            var janelaDeEdicao = new NewWindow(button);
            janelaDeEdicao.Closed += JanelaDeEdicao_Closed;
            janelaDeEdicao.ShowDialog(this);
    }
*/
    private void JanelaDeEdicao_Closed(object sender, EventArgs e)
    {
        A.ConsultarBotoesESalvar(StakBTN, Conexao);
    }
    
    private void btnCHECAR_Click(object sender, RoutedEventArgs e)
    {
    }
    
    private void btnTESTE_Click(object sender, RoutedEventArgs e)
    {
        TextX.Text = $"{TextX.Text}"+$"{P.GetPastaDoUsuario()}\n";
    }
    
    private void btnLIMPAR_Click(object sender, RoutedEventArgs e)
    {
        TextX.Text = "";
    }

    public void AdicionarBotao(Button botao)
    {
        var stackPanel = this.FindControl<StackPanel>("StakBTN");
        stackPanel.Children.Add(botao);
    }
}

