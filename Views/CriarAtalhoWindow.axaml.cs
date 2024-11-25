using System.Diagnostics;
using Avalonia.Controls;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.IO;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System;
using Dock.Views;


namespace Dock.Views{

    public partial class CriarAtalhoWindow : Window
    {
        private readonly MainWindow _mainWindow;

        public CriarAtalhoWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            Debug.WriteLine("BoasVindasWindow initialized."); // Adicione esta linha
            
            // Garanta que a janela seja visível e posicionada corretamente
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.IsVisible = true;

            IntroText.FontSize = 18;
            IntroText.Width = 500;
            IntroText.Height = 200;
            IntroText.Text = "->Olá caro usuário!\n" +
                             "->Clique em: Escolher Icone do Atalho, para escolher um icone em formato PNG;\n" +
                             "->Clique em: Escolher Programa, para escolher o local programa que deseja anexar ao Atalho na Dock;\n" +
                             "->No campo Nome do programa digite um nome desejado para o Atalho;\n" +
                             "->Por fim Clique em Adicionar Botão, para adicionar um botão novo a Dock";
        }

        private void AdicionarBotao_Click(object sender, RoutedEventArgs e)
        {
            var nomeDoBotao = NomeBotao.Text;
            var botao = new Button
            {
                Content = NomeBotao.Text,
                Width = 100,
                Height = 100,
                BorderBrush= Brushes.White,
                CornerRadius=new CornerRadius(60),
                BorderThickness = new Thickness(0),
                Margin = new Thickness(0),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Background = new ImageBrush(new Bitmap(IconePNG.Text)),
                FontSize = 0.1,
                Tag = new Dictionary<string, string>
                {
                    { "CaminhoDoPrograma", $"{CaminhoPrograma.Text}" }, { "IconePng", $"{IconePNG.Text}" }
                }
            };
            StackPanel STK = new StackPanel();
            
            // Criar e configurar o ContextMenu
            var contextMenu = new ContextMenu();
            var editMenuItem = new MenuItem { Header = "Editar" };
            editMenuItem.Click += (s, e) => AbrirJanelaDeEdicao(botao, _mainWindow.StakBase);
            contextMenu.Items.Add(editMenuItem);
        
            // Associar o ContextMenu ao botão
            botao.ContextMenu = contextMenu;
            // Adicionar o botão na MainWindow
            _mainWindow.AdicionarBotao(botao, CaminhoPrograma.Text, STK);
            this.Close();
        }
        
        private void AbrirJanelaDeEdicao(Button button, StackPanel stackpanel)
        { 
            var janelaDeEdicao = new NewWindow(button, stackpanel);
            janelaDeEdicao.Show();
        }
        
        private async void OpenFileCaminhoPrograma_Clicked(object? sender, RoutedEventArgs e)
        {
    
            var topLevel = TopLevel.GetTopLevel(this);

            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Text File",
                AllowMultiple = false
            });

            if (files.Count >= 1)
            {
                await using var stream = await files[0].OpenReadAsync();
                using var streamReader = new StreamReader(stream);
                var fileContent = await streamReader.ReadToEndAsync();
            }
    
            // Carregar dados do arquivo de texto
            CaminhoPrograma.Text = files[0].Path.LocalPath;
        }
        
        private async void OpenFileiconePNG_Clicked(object? sender, RoutedEventArgs e)
        {
    
            var topLevel = TopLevel.GetTopLevel(this);

            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Text File",
                AllowMultiple = false
            });

            if (files.Count >= 1)
            {
                await using var stream = await files[0].OpenReadAsync();
                using var streamReader = new StreamReader(stream);
                var fileContent = await streamReader.ReadToEndAsync();
            }
    
            // Carregar dados do arquivo de texto
            try 
            {
                IconePNG.Text = files[0].Path.LocalPath;
                // Se chegou aqui, o caminho é válido
            }
            catch (Exception ex)
            {
                // Tratar a exceção (ex. exibir mensagem de erro)
            }
            
            
        }
    }
}
