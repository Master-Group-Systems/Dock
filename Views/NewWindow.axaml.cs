using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Dock.Views{

    public partial class NewWindow : Window
    {
        public NewWindow()
        {
            InitializeComponent();

        }

        private void ToggleButton_Click(object? sender, RoutedEventArgs e)
        {
            // Alterna o conte�do do bot�o entre "AI" e "N"
            if (ToggleButton.Content?.ToString() == "AI")
            {
                ToggleButton.Content = "N";
            }
            else
            {
                ToggleButton.Content = "AI";
            }
        }
        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            // Fechar a janela
            this.Close();
        }
    }
}
