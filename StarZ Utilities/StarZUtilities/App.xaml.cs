using StarZUtilities.Classes;
using System;
using System.IO;
using System.Net;
using System.Windows;

namespace StarZUtilities;

// class to create the launcher's folders in documents + check for launcher's version + call the DiscordRPC class for initializations based on the settings
public partial class App : Application
{
    bool HasRun = false;
    private const string? VERSION_URL = "https://raw.githubusercontent.com/ignYoqzii/StarZUtilities/main/Version.txt";

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        string documentspath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string starzutilitiespath = Path.Combine(documentspath, "StarZ Utilities");
        string versionfilepath = Path.Combine(starzutilitiespath, "Version.txt");
        string musicspath = Path.Combine(starzutilitiespath, "Musics");
        string logspath = Path.Combine(starzutilitiespath, "Logs");
        if (!Directory.Exists(starzutilitiespath))
        {
            Directory.CreateDirectory(starzutilitiespath);
        }
        if (!Directory.Exists(musicspath))
        {
            Directory.CreateDirectory(musicspath);
        }
        if (!Directory.Exists(logspath))
        {
            Directory.CreateDirectory(logspath);
        }
        if (!File.Exists(versionfilepath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(versionfilepath));
            DownloadLatestVersion(versionfilepath); // Download the latest version from the URL and save it to the file
        }

        if (!HasRun)
        {
            bool discordRPC = ConfigManager.GetDiscordRPC();
            if (discordRPC == true)
            {
                DiscordRichPresenceManager.DiscordClient.Initialize();
                DiscordRichPresenceManager.SetPresence();
                HasRun = true;
            }
            return;
        }
    }

    private static void DownloadLatestVersion(string filePath)
    {
        using WebClient client = new();
        string? latestVersion = client.DownloadString(VERSION_URL).Trim();
        File.WriteAllText(filePath, latestVersion); // Write the latest version to the file
    }
    private void App_OnExit(object sender, ExitEventArgs e) => DiscordRichPresenceManager.TerminatePresence();
}
