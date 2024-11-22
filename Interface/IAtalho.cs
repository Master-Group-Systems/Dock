using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Interactivity;
using Dock.Models;

namespace Dock.Interface;

public interface IAtalho
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CaminhoDoPrograma { get; set; }
    public string IconePng { get; set; }

    public List<Button> CriarBotoes(List<Atalho> atalhos);

    public void Botao_Click(object sender, RoutedEventArgs e, string caminhoPrograma);

    public List<Atalho> CarregarAtalhos(string conexao);
}