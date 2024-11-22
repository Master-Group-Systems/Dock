using System;
using System.IO;
using Dock.Interface;
namespace Dock.Models;

public class PathDir : IPathDir
{
    public string GetPastaDoUsuario()
    {
        string UsuarioPastaDeDiretorio;

        if (Environment.OSVersion.Platform == PlatformID.Win32NT) // Para Windows
        {
            UsuarioPastaDeDiretorio = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".dock");
        }
        else if (Environment.OSVersion.Platform == PlatformID.Unix) // Para Linux ou macOS
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            UsuarioPastaDeDiretorio = Path.Combine(homeDirectory, ".dock");
        }
        else
        {
            throw new NotSupportedException("Sistema operacional não suportado.");
        }

        if (!Directory.Exists(UsuarioPastaDeDiretorio))
        {
            Directory.CreateDirectory(UsuarioPastaDeDiretorio);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT) // Para Windows
            {
                File.SetAttributes(UsuarioPastaDeDiretorio, File.GetAttributes(UsuarioPastaDeDiretorio) | FileAttributes.Hidden);
            }
        }

        return UsuarioPastaDeDiretorio;
    }
}