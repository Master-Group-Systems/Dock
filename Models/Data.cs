using System.Net;
using Microsoft.Data.Sqlite;
using Dock.Interface;
using System.IO;

namespace Dock.Models;

public class Data : IData
{
    static PathDir P = new PathDir();
    
    
    // Cria o banco de dados no caminho especificado em 'conexao'.
    public void CriarCanco(string conexao)
    {
        using (var database = new SqliteConnection(conexao))
        {
            database.Open();

            string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Dock (
            BG_COLOR TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Atalho (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            NOME TEXT NOT NULL,
            Icone_PNG TEXT NOT NULL,
            CAMINHO_DO_PROGRAMA TEXT NOT NULL
            );
            ";

            using (var command = new SqliteCommand(createTableQuery, database))
            {
                command.ExecuteNonQuery();
                //Debug_Text.AppendText("Tabela 'Pessoas' criada ou já existente.\n");
            }
        }
    }
    public void Recuperarbanco()
    {
        
    }

    public void Salvarbanco()
    {
       
    }
    // Verifica se o arquivo meu_banco_de_dados.db presente na pasta .dock existe
    public string VerificarBanco()
    {
        string arquivoBanco = Path.Combine(P.GetPastaDoUsuario(), "meu_banco_de_dados.db");

        if (File.Exists(arquivoBanco))
        {
            return "encontrado";
        }
        else
        {
            return "nao_encontrado";
        }
    }
}