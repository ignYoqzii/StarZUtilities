using StarZUtilities.Windows;
using System;
using System.IO;
using System.Net;
using System.Windows.Controls;
using System.DirectoryServices.AccountManagement;
using System.Windows.Media.Imaging;
using System.Windows;
using static StarZUtilities.Classes.MusicPlayer;
using static StarZUtilities.Windows.MainWindow;
using System.Threading.Tasks;
using Woof.SystemEx;

namespace StarZUtilities.Classes
{
    public partial class Loader
    {

        public static void CheckForThemes()
        {
            string Mode = ConfigManager.GetTheme();
            if (Mode == "Light")
            {
                ThemesManager.LoadTheme("LightMode.xaml");
                LightModeCheckBox!.IsChecked = true;
                LightModeCheckBox!.IsEnabled = false;
            }
            else if (Mode == "Dark")
            {
                ThemesManager.LoadTheme("DarkMode.xaml");
                DarkModeCheckBox!.IsChecked = true;
                DarkModeCheckBox!.IsEnabled = false;
            }
        }

        public static void Load()
        {
            VersionHelper.LoadCurrentVersions();
            VersionHelper.CheckForUpdates();
            Task.Run(() => LoadUserProfileInfo());
        }

        public static void LoadMusicFiles()
        {
            MusicItems.Clear(); // Refreshes the list in case

            string[] musicFiles = Directory.GetFiles(musicDirectoryPath, "*.mp3");

            foreach (string filePath in musicFiles)
            {
                MusicItem musicItem = new(filePath);
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

        public static async Task LoadUserProfileInfo()
        {
            await Task.Run(() =>
            {
                // Get user display name
                UserPrincipal userPrincipal = UserPrincipal.Current;
                string displayName = userPrincipal?.DisplayName ?? Environment.UserName;

                // Update TextBlock with user display name on the UI thread
                Application.Current.Dispatcher.Invoke(() =>
                {
                    usernameTextBlock!.Text = displayName;
                });
            });

            await GetUserProfileImage();
        }

        private static async Task GetUserProfileImage()
        {
            try
            {
                var userPicturePath = await Task.Run(() => new BitmapImage(new Uri(SysInfo.GetUserPicturePath())));

                userPicturePath.Freeze(); // Freezing for thread safety

                profilePictureImage!.Dispatcher.Invoke(() =>
                {
                    profilePictureImage!.Source = userPicturePath;
                });
            }
            catch (Exception ex)
            {
                // Handle exception (log or display error)
                Application.Current.Dispatcher.Invoke(() =>
                {
                    StarZMessageBox.ShowDialog($"Error loading user image: {ex.Message}", "Error", false);
                });
            }
        }
    }
}
