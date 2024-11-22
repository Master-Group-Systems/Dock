using System.Net;
using Microsoft.Data.Sqlite;
using Dock.Interface;
using System.IO;

namespace Dock.Models;

public class Data : IData
{
    static PathDir P = new PathDir();
    public void CriarCanco(string conexao)
    {
        using (var database = new SqliteConnection(conexao))
        {
            database.Open();

            string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Pessoas (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL,
            Idade INTEGER NOT NULL
            );";

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