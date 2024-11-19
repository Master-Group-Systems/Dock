using System;
using System.IO;
using Dock.Interface;
namespace Dock.Models;

public class PathDir : IPathDir
{
    public string GetUserDataDirectory()
    {
        string userDataDirectory;

        if (Environment.OSVersion.Platform == PlatformID.Win32NT) // Para Windows
        {
            userDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".Dock");
        }
        else if (Environment.OSVersion.Platform == PlatformID.Unix) // Para Linux ou macOS
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            userDataDirectory = Path.Combine(homeDirectory, ".Dock");
        }
        else
        {
            throw new NotSupportedException("Sistema operacional não suportado.");
        }

        if (!Directory.Exists(userDataDirectory))
        {
            Directory.CreateDirectory(userDataDirectory);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT) // Para Windows
            {
                File.SetAttributes(userDataDirectory, File.GetAttributes(userDataDirectory) | FileAttributes.Hidden);
            }
        }

        return userDataDirectory;
    }
}