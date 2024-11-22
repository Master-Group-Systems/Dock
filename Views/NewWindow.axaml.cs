using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Dock.Views{

    public partial class BoasVindasWindow : Window
    {
        public BoasVindasWindow()
        {
            InitializeComponent();
        }

        private void ToggleButton_Click(object? sender, RoutedEventArgs e)
        {
        }
        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            // Fechar a janela
            this.Close();
        }
    }
}
