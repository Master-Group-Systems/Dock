using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using System;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using Dock.Interface;

namespace Dock.Models;

public class Atalho : IAtalho
{
    public int Id { get; set; }                     // Propriedade. Variável privada.
    public string Nome { get; set; }                // Propriedade. Variável privada.
    public string CaminhoDoPrograma { get; set; }   // Propriedade. Variável privada.
    public string IconePng { get; set; }            // Propriedade. Variável privada.
    
    public List<Button> CriarBotoes(List<Atalho> atalhos)
    {
        var botoes = new List<Button>();

        foreach (var atalho in atalhos)
        {
            var botao = new Button
            {
                Content = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Children =
                    {
                        new Image
                        {
                            Source = new Bitmap(atalho.IconePng),
                            Width = 50,
                            Height = 50
                        },
                        new TextBlock
                        {
                            Text = atalho.Nome,
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                        }
                    }
                },
                Background = Brushes.LightBlue,
                Width = 100,
                Height = 100,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
            };

            botao.Click += (sender, e) => Botao_Click(sender, e, atalho.CaminhoDoPrograma);

            botoes.Add(botao);
        }

        return botoes;
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
    
    public List<Atalho> CarregarAtalhos(string conexao)
    {
        var atalhos = new List<Atalho>();

        using (var connection = new SqliteConnection(conexao))
        {
            connection.Open();

            string selectQuery = "SELECT Id, Nome, Caminho_Do_Programa, Icone_PNG FROM Atalho;";
            using (var command = new SqliteCommand(selectQuery, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    atalhos.Add(new Atalho
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
    
   public void InserirDadosDeTeste(string _connectionString)
    {
        using (var connection = new SqliteConnection(_connectionString))
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
}