using System;
using System.IO;
using Dock.Interface;
namespace Dock.Models;

public class PathDir : IPathDir
{
    // Método retorna o caminho da pasta .dock. Caso não exista, a cria.
    public string GetPastaDoUsuario()
    {
        string UsuarioPastaDeDiretorio;

        // Windows
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            UsuarioPastaDeDiretorio = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".dock");
        }
        // Unix
        else if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            UsuarioPastaDeDiretorio = Path.Combine(homeDirectory, ".dock");
        }
        // Outros sistemas operacionais 
        else
        {
            throw new NotSupportedException("Sistema operacional não suportado.");
        }

        // Caso o caminho UsuarioPastaDeDiretorio não exista, o cria
        if (!Directory.Exists(UsuarioPastaDeDiretorio))
        {
            Directory.CreateDirectory(UsuarioPastaDeDiretorio);

            // Windows: Oculta a pasta
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                File.SetAttributes(UsuarioPastaDeDiretorio, File.GetAttributes(UsuarioPastaDeDiretorio) | FileAttributes.Hidden);
            }
        }

        return UsuarioPastaDeDiretorio;
    }
}