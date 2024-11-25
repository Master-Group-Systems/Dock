using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Interactivity;
using Dock.Models;
using Dock.Objects;

namespace Dock.Interface;

public interface IAtalho
{
    List<Button> CriarBotoes(List<OTalho> atalhos, StackPanel stackpanel);

    void Botao_Click(object sender, RoutedEventArgs e, string caminhoPrograma);

    List<OTalho> CarregarAtalhos(string conexao);
    
    void ConsultarBotoesESalvar(StackPanel stackPanel, string conexao);
}