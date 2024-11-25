using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Dock.Models;

namespace Dock.Views{
    
    public partial class NewWindow : Window
    {
        private Button _button;
        private StackPanel _stackPanel;
        
        private static PathDir P = new PathDir();
        private static Data D = new Data();
        private static Atalho A = new Atalho();
        
        private string Conexao = "Data Source="+$"{P.GetPastaDoUsuario()}"+"\\meu_banco_de_dados.db";

        public NewWindow(Button button, StackPanel stackPanel)
        {
            InitializeComponent();
            _button = button;
            _stackPanel = stackPanel;
            
            // Inicializar campos com valores atuais
            NomeTextBox.Text = _button.Content.ToString();
            
            if (_button.Tag is Dictionary<string, string> tagData && tagData.TryGetValue("CaminhoDoPrograma", out var caminhoDoPrograma))
            {
                CaminhoTextBox.Text = caminhoDoPrograma;
            }
            if (_button.Tag is Dictionary<string, string> tagData2 && tagData2.TryGetValue("IconePng", out var IconePng))
            {
                CaminhoIconeTextBox.Text = IconePng;
            }
        }

        private void Salvar_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _button.Content = NomeTextBox.Text;

            if (_button.Tag is Dictionary<string, string> tagData)
            {
                tagData["CaminhoDoPrograma"] = CaminhoTextBox.Text;
            }
            else
            {
                _button.Tag = new Dictionary<string, string>
                {
                    { "CaminhoDoPrograma", CaminhoTextBox.Text }
                };
            }
            
            if (_button.Tag is Dictionary<string, string> tagData2)
            {
                tagData2["IconePng"] = CaminhoIconeTextBox.Text;
            }
            else
            {
                _button.Tag = new Dictionary<string, string>
                {
                    { "IconePng", CaminhoIconeTextBox.Text }
                };
            }
            D.DropTableAtalho(Conexao);
            A.ConsultarBotoesESalvar(_stackPanel, Conexao);
            _stackPanel.Children.Clear();
            var botoes = A.CriarBotoes(A.CarregarAtalhos(Conexao), _stackPanel);
            
            foreach (var botao in botoes)
            {
                _stackPanel.Children.Add(botao);
            }
            
            this.Close();
        }
    }
}

