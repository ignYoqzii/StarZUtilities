using StarZUtilities.Windows;
using System;
using System.IO;
using System.Net;
using System.Windows;
using static StarZUtilities.Classes.MusicPlayer;
using static StarZUtilities.Windows.MainWindow;

namespace StarZUtilities.Classes
{
    public partial class Loader
    {
        public static void CheckForThemes()
        {
            string Mode = ConfigManager.GetTheme();
            if (Mode == "Light")
            {
                ThemesManager.LoadLightMode();
                LightModeCheckBox!.IsChecked = true;
                LightModeCheckBox!.IsEnabled = false;
            }
            else if (Mode == "Dark")
            {
                ThemesManager.LoadDarkMode();
                DarkModeCheckBox!.IsChecked = true;
                DarkModeCheckBox!.IsEnabled = false;
            }
        }

        public static void CheckForColors()
        {

        }

        public static void LoadMusicFiles()
        {
            MusicItems.Clear(); // Refreshes the list in case

            string[] musicFiles = Directory.GetFiles(musicDirectoryPath, "*.mp3");

            foreach (string filePath in musicFiles)
            {
                MusicItem musicItem = new MusicItem(filePath);
                MusicItems.Add(musicItem);
            }
            CheckForItemsCount();
        }
        public static void GetIpAddress()
        {
            try
            {
                // Get the local machine's IP addresses
                string hostName = Dns.GetHostName();
                IPAddress[] localIPs = Dns.GetHostAddresses(hostName);

                // Find the IPv4 address (assuming one exists)
                foreach (IPAddress ip in localIPs)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipaddressTextBlock!.Dispatcher.Invoke(() =>
                        {
                            ipaddressTextBlock!.Text = $"IP Address: {ip}";
                        });
                        return;
                    }
                }

                // If no IPv4 address found
                ipaddressTextBlock!.Dispatcher.Invoke(() =>
                {
                    ipaddressTextBlock!.Text = "IP Address: Not Found";
                });
            }
            catch (Exception ex)
            {
                StarZMessageBox.ShowDialog($"Error getting IP Address: {ex.Message}", "Error", false);
            }
        }

        private static void CheckForItemsCount()
        {
            if (MusicItems.Count == 0)
            {
                MusicPlayerInformationTextBlock!.Visibility = Visibility.Visible;
            }
            else
            {
                MusicPlayerInformationTextBlock!.Visibility = Visibility.Collapsed;
            }
        }

        public static void CreateDirectory()
        {
            string documentspath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string starzutilitiespath = Path.Combine(documentspath, "StarZ Utilities");
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
        }
    }
}
