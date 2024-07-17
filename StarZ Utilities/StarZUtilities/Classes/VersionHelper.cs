using Newtonsoft.Json.Linq;
using StarZUtilities.Windows;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using static StarZUtilities.Windows.MainWindow;

namespace StarZUtilities.Classes
{
    public static class VersionHelper
    {
        public static string? Version { get; private set; }

        private const string? VERSION_FILE_PATH = @"StarZ Utilities\Version.txt";
        private const string? VERSION_URL = "https://raw.githubusercontent.com/ignYoqzii/StarZUtilities/main/Version.txt";
        private const string? DOWNLOAD_URL = "https://github.com/ignYoqzii/StarZUtilities/releases/download/{0}/StarZUtilities.exe";

        static VersionHelper()
        {
            // get the launcher's version from github
            string? url = "https://raw.githubusercontent.com/ignYoqzii/StarZUtilities/main/Version.txt";

            try
            {
                WebClient client = new();
                var getVersion = client.DownloadString(url);
                Version = getVersion?.Replace("\n", "");
            }
            catch (Exception)
            {
                Version = "Error";
            }
        }

        // Updater part of the launcher
        public static void CheckForUpdates()
        {
            string? currentVersion = GetCurrentVersion();
            string? latestVersion = GetLatestVersion();

            if (currentVersion == latestVersion)
            {
                // Do nothing - program is up to date
            }
            else
            {
                bool? result = StarZMessageBox.ShowDialog("A new update is available. Click OK to update the program to the latest, or CANCEL to ignore and keep using an outdated version.", "New update available !");

                if (result == true)
                {
                    string downloadLink = string.Format(DOWNLOAD_URL, latestVersion);
                    DownloadLatestVersion(downloadLink);
                }
            }
        }

        private static string GetCurrentVersion()
        {
            string? versionFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), VERSION_FILE_PATH);
            return File.ReadAllText(versionFilePath).Trim();
        }

        private static string GetLatestVersion()
        {
            using WebClient client = new();
            string? versionString = client.DownloadString(VERSION_URL);
            return versionString.Trim();
        }

        private static void DownloadLatestVersion(string url)
        {
            string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            var fileName = $"StarZ Utilities ({Version}).exe";
            string filePath = Path.Combine(downloadPath, fileName);

            // Check if the file exists on the desktop and delete it if it does
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Download the latest version of StarZ Launcher
            using WebClient client = new();
            client.DownloadFile(new Uri(url), filePath);

            // Update the version file with the latest version number
            string versionFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), VERSION_FILE_PATH);
            string latestVersion = client.DownloadString(VERSION_URL).Trim();
            File.WriteAllText(versionFilePath, latestVersion);

            // Close the application
            Application.Current.Shutdown();
        }

        // End of updater part

        public static void LoadCurrentVersions()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                version1TextBlock!.Text = $"{Version}";
            });
        }
    }
}

