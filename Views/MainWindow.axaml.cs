using System;
using System.Diagnostics;
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
    private static Data D = new Data();
    private static PathDir P = new PathDir();
    private static Atalho A = new Atalho();
    private Point _pontoinicial;
    private bool _isDragging = false;
    
    private string Conexao = "Data Source="+$"{P.GetPastaDoUsuario()}"+"\\meu_banco_de_dados.db";
    
    
    public MainWindow()
    {
        InitializeComponent();
        
        this.DataContext = new MainWindowViewModel();
        this.PointerPressed += PonteiroPrecionado;
        this.PointerMoved += PonteiroMovido;
        this.PointerReleased += PonteiroSolto;
        this.Loaded += MainWindow_Loaded;
        
        // Criar e configurar o ContextMenu
        var contextMenu = new ContextMenu();
        var editMenuItem = new MenuItem { Header = "Criar Atalho"};
        editMenuItem.Click += (s, e) => AbrirJanelaDeEdicao();
        contextMenu.Items.Add(editMenuItem);
        
        // Associar o ContextMenu ao botão
        this.ContextMenu = contextMenu;

    }
    private void AbrirJanelaDeEdicao()
    { 
        var janelaDeEdicao = new CriarAtalhoWindow(this);
        janelaDeEdicao.Closed += JanelaDeEdicao_Closed;
        janelaDeEdicao.Show();
    }
    private void JJanelaDeEdicao_Closed(object sender, EventArgs e)
    {
        D.DropTableAtalho(Conexao);
        A.ConsultarBotoesESalvar(this.StakBase, Conexao);
        this.StakBase.Children.Clear();
        var botoes = A.CriarBotoes(A.CarregarAtalhos(Conexao), this.StakBase);
            
        foreach (var botao in botoes)
        {
            this.StakBase.Children.Add(botao);
        }
    }
    
    
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {

        if (D.VerificarBanco() == "nao_encontrado")
        {
            //Criar Banco de dados;
            D.CriarBanco(Conexao);
            
            //Abre a janela de boas vindas
            var boasVindasJanela = new BoasVindasWindow(this);
            boasVindasJanela.Closed += boasVindasJanela_Closed;
            boasVindasJanela.Show();
           

        }
        else if (D.VerificarBanco() == "encontrado")
        {   
            var close = new Button{
                Background = new ImageBrush(new Bitmap($"Assets//icon-close.png")),
                Width = 80,
                Height = 80,
                BorderBrush= Brushes.White,
                CornerRadius=new CornerRadius(40),
                BorderThickness = new Thickness(0),
                Margin = new Thickness(0),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                FontSize = 0.1,
            };
            PanelBase.Children.Add(close);
            close.Click += OnButtonClick;
            var note = new Button{
                Background = new ImageBrush(new Bitmap($"Assets\\icons8-notas-80.png")),
                Width = 80,
                Height = 80,
                BorderBrush= Brushes.White,
                CornerRadius=new CornerRadius(40),
                BorderThickness = new Thickness(0),
                Margin = new Thickness(0),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                FontSize = 0.1
            };
            PanelBase.Children.Add(note);
            
            var botoes = A.CriarBotoes(A.CarregarAtalhos(Conexao), StakBase);
            // Lógica para banco de dados não encontrado
            foreach (var botao in botoes)
            {
                StakBase.Children.Add(botao);
            }
        }
    }
    
    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void boasVindasJanela_Closed(object sender, EventArgs e)
    {
        A.ConsultarBotoesESalvar(StakBase, Conexao);
    }
    
    public void AdicionarBotao(Button botao, string CaminhoPrograma, StackPanel stackpanel)
    {
        var stackPanel = this.FindControl<StackPanel>("StakBase");
        
        botao.Click += (sender, e) => Botao_Click(sender, e, CaminhoPrograma);
        
        var contextMenu = new ContextMenu();
        var editMenuItem = new MenuItem { Header = "Editar" };
        editMenuItem.Click += (s, e) => AbrirJanelaDeEdicao(botao, stackpanel);        
        stackPanel.Children.Add(botao);
    }
    
    public void AdicionarBotao(Button botao, Panel panel){

    }

    private void AbrirJanelaDeEdicao(Button button, StackPanel stackpanel)
    {
        var janelaDeEdicao = new NewWindow(button, stackpanel);
        janelaDeEdicao.Closed += JanelaDeEdicao_Closed;
        janelaDeEdicao.ShowDialog(this);
    }
    
    private void JanelaDeEdicao_Closed(object sender, EventArgs e)
    {
        D.DropTableAtalho(Conexao);
        A.ConsultarBotoesESalvar(StakBase, Conexao);
    }
    public void Botao_Click(object sender, RoutedEventArgs e, string caminhoPrograma)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = caminhoPrograma,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao abrir o programa: {ex.Message}");
        }
    }

    private void PonteiroPrecionado(object sender, PointerPressedEventArgs e)
    {
        if (e.Source is not Button)
        {
            _pontoinicial = e.GetPosition(this);
            _isDragging = true;
        }
    }

    private void PonteiroMovido(object sender, PointerEventArgs e)
    {
        if (_isDragging && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            var position = e.GetPosition(this);
            var deltaX = position.X - _pontoinicial.X;
            var deltaY = position.Y - _pontoinicial.Y;
            Position = new PixelPoint((int)(Position.X + deltaX), (int)(Position.Y + deltaY));
        }
    }

    private void PonteiroSolto(object sender, PointerReleasedEventArgs e)
    {
        _isDragging = false;
        // Redefine a posição inicial para evitar movimentos indesejados
        _pontoinicial = new Point();
    }

}