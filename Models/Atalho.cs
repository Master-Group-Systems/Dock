using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia;
using Avalonia.Layout;
using System;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using Avalonia.Xaml.Interactions.Custom;
using Dock.Interface;
using Dock.Objects;
using HarfBuzzSharp;
using Dock.Views;

namespace Dock.Models;

public class Atalho : IAtalho
{
    public List<Button> CriarBotoes(List<OTalho> atalhos, StackPanel stackpanel)
    {
        var botoes = new List<Button>();

        foreach (var atalho in atalhos)
        {
            var botao = new Button
            {
                Content = atalho.Nome,
                Background = new ImageBrush(new Bitmap($"{atalho.IconePng}")),
                Width = 80,
                Height = 80,
                BorderBrush= Brushes.White,
                CornerRadius=new CornerRadius(40),
                BorderThickness = new Thickness(0),
                Margin = new Thickness(0),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                FontSize = 0.1,
                Tag = new Dictionary<string, string>
                {
                    { "CaminhoDoPrograma", $"{atalho.CaminhoDoPrograma}" }, { "IconePng", $"{atalho.IconePng}" }
                }
            };

            botao.Click += (sender, e) => Botao_Click(sender, e, atalho.CaminhoDoPrograma);
            
            // Criar e configurar o ContextMenu
            var contextMenu = new ContextMenu();
            var editMenuItem = new MenuItem { Header = "Editar" };
            editMenuItem.Click += (s, e) => AbrirJanelaDeEdicao(botao, stackpanel);
            contextMenu.Items.Add(editMenuItem);
        
            // Associar o ContextMenu ao botão
            botao.ContextMenu = contextMenu;

            botoes.Add(botao);
        }

        return botoes;
    }
    
    private void AbrirJanelaDeEdicao(Button button, StackPanel stackpanel)
    { 
        var janelaDeEdicao = new NewWindow(button, stackpanel);
        janelaDeEdicao.Show();
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
    
    

    public List<OTalho> CarregarAtalhos(string conexao)
    {
        var atalhos = new List<OTalho>();

        using (var connection = new SqliteConnection(conexao))
        {
            connection.Open();

            string selectQuery = "SELECT Id, Nome, Caminho_Do_Programa, Icone_PNG FROM Atalho;";
            using (var command = new SqliteCommand(selectQuery, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    atalhos.Add(new OTalho
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        CaminhoDoPrograma = reader.GetString(2),
                        IconePng = reader.GetString(3)
                    });
                }
            }
        }

        return atalhos;
    }
    
// metodo abaixo para fins de teste Teste
    public void InserirDadosDeTeste(string conexao)
    {
        using (var connection = new SqliteConnection(conexao))
        {
            connection.Open();

            string insertQuery = @"
                INSERT INTO Atalho (NOME, Icone_PNG, CAMINHO_DO_PROGRAMA) VALUES (@Nome, @IconePng, @CaminhoDoPrograma);
            ";

            var comandos = new List<SqliteCommand>
            {
                new SqliteCommand(insertQuery, connection)
                {
                    Parameters = {
                        new SqliteParameter("@Nome", "Bloco de Notas"),
                        new SqliteParameter("@IconePng", "Assets/facebook.png"), // Certifique-se de que este caminho existe
                        new SqliteParameter("@CaminhoDoPrograma", "notepad.exe")
                    }
                },
                new SqliteCommand(insertQuery, connection)
                {
                    Parameters = {
                        new SqliteParameter("@Nome", "Calculadora"),
                        new SqliteParameter("@IconePng", "Assets/discordia.png"), // Certifique-se de que este caminho existe
                        new SqliteParameter("@CaminhoDoPrograma", "calc.exe")
                    }
                },
                new SqliteCommand(insertQuery, connection)
                {
                    Parameters = {
                        new SqliteParameter("@Nome", "Paint"),
                        new SqliteParameter("@IconePng", "Assets/spotify.png"), // Certifique-se de que este caminho existe
                        new SqliteParameter("@CaminhoDoPrograma", "mspaint.exe")
                    }
                }
            };

            foreach (var comando in comandos)
            {
                comando.ExecuteNonQuery();
            }
        }
    } 
//
    
    
// Salvar botoes
    public void ConsultarBotoesESalvar(StackPanel stackPanel, string conexao)
    {
        var _botoesDados = new List<OTalho>();

        foreach (var control in stackPanel.Children)
        {
            if (control is Button botao)
            {
                string icon = "";
                string path = "";

                var tagData = botao.Tag as Dictionary<string, string>;

                if (tagData.TryGetValue("IconePng", out string iconePng))
                { 
                    icon = iconePng; 
                }

                if (tagData.TryGetValue("CaminhoDoPrograma", out string CaminhoDoPrograma))
                { 
                    path = CaminhoDoPrograma; 
                }

                var botaoDados = new OTalho
                {
                    Nome = botao.Name, 
                    IconePng = icon,
                    CaminhoDoPrograma = path
                };

                _botoesDados.Add(botaoDados);
            }
        }
        SalvarDadosNoBanco(_botoesDados, conexao);
    }

    private void SalvarDadosNoBanco(List<OTalho> botoesDados, string conexao)
    {
        try
        {
            using (var connection = new SqliteConnection(conexao))
            {
                connection.OpenAsync();

                string insertQuery = @"
                INSERT INTO Atalho (NOME, CAMINHO_DO_PROGRAMA, Icone_PNG) 
                VALUES (@Nome, @CaminhoDoPrograma, @IconePng);
            ";

                foreach (var dados in botoesDados)
                {
                    using (var command = new SqliteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", dados.Nome ?? "");
                        command.Parameters.AddWithValue("@CaminhoDoPrograma", dados.CaminhoDoPrograma ?? "");
                        command.Parameters.AddWithValue("@IconePng", dados.IconePng ?? "");

                        command.ExecuteNonQueryAsync();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving to database: {ex.Message}");
            // Consider logging the full exception details for debugging
        }
    }

    
    
    
    
    
}