using StarZUtilities.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace StarZUtilities.Windows
{
    public partial class MainWindow : Window
    {
        // Making them public to be used somewhere else
        public static Image? HomeTabImage;
        public static Image? CleanTabImage;
        public static Image? AboutTabImage;
        public static Image? MusicPlayerTabImage;
        public static Image? SettingsTabImage;
        public static Image? StarZLogoImage;
        public static Image? profilePictureImage;

        public static TabControl? MainTabControl;
        public static TabItem? HometabItem;
        public static TabItem? CleantabItem;
        public static TabItem? AbouttabItem;
        public static TabItem? MusicPlayertabItem;
        public static TabItem? SettingstabItem;

        public static Border? BackgroundForWindowsOnTop;

        public static CheckBox? DarkModeCheckBox;
        public static CheckBox? LightModeCheckBox;

        public static TextBlock? MusicPlayerInformationTextBlock;
        public static TextBlock? usernameTextBlock;
        public static TextBlock? cpuTempTextBlock;
        public static TextBlock? gpuTempTextBlock;
        public static TextBlock? cpuNameTextBlock;
        public static TextBlock? gpuNameTextBlock;
        public static TextBlock? cpuLoadTextBlock;
        public static TextBlock? gpuLoadTextBlock;
        public static TextBlock? cpufanTextBlock;
        public static TextBlock? gpufanTextBlock;
        public static TextBlock? memoryTextBlock;
        public static TextBlock? motherboardTextBlock;
        public static TextBlock? ipaddressTextBlock;
        public static TextBlock? version1TextBlock;

        public static TextBox? URLTextBox;
        public static TextBox? statusTextBox;

        public static Image? CloseImage;
        public static Image? MinimizeImage;

        private HardwareMonitor hardwareMonitor;

        public MainWindow()
        {
            InitializeComponent();

            hardwareMonitor = new HardwareMonitor();

            // Start the HardwareMonitoring
            Task.Run(() => hardwareMonitor.StartMonitoring());

            // Images
            HomeTabImage = HomeTab;
            CleanTabImage = CleanTab;
            AboutTabImage = AboutTab;
            MusicPlayerTabImage = MusicPlayerTab;
            SettingsTabImage = SettingsTab;
            StarZLogoImage = StarZLogo;
            profilePictureImage = ProfilePictureImage;

            // TabControl and Items
            MainTabControl = SideBarTabControl;
            HometabItem = HomeTabItem;
            CleantabItem = CleanTabItem;
            AbouttabItem = AboutTabItem;
            MusicPlayertabItem = MusicPlayerTabItem;
            SettingstabItem = SettingsTabItem;

            // Window background
            BackgroundForWindowsOnTop = DarkBackgroundWindows;

            // Themes checkboxes
            DarkModeCheckBox = CheckBoxDarkMode;
            LightModeCheckBox = CheckBoxLightMode;

            // Individual TextBlocks
            MusicPlayerInformationTextBlock = MusicPlayerInfoTextBlock;
            usernameTextBlock = UsernameTextBlock;
            cpuTempTextBlock = CPUTempTextBlock;
            gpuTempTextBlock = GPUTempTextBlock;
            gpuNameTextBlock = GPUNameTextBlock;
            cpuNameTextBlock = CPUNameTextBlock;
            gpuLoadTextBlock = GPULoadTextBlock;
            cpuLoadTextBlock = CPULoadTextBlock;
            cpufanTextBlock = CPUFanTextBlock;
            gpufanTextBlock = GPUFanTextBlock;
            memoryTextBlock = MemoryTextBlock;
            motherboardTextBlock = MotherboardTextBlock;
            ipaddressTextBlock = IPAddressTextBlock;
            version1TextBlock = Version1TextBlock;

            // Buttons
            CloseImage = CloseButton;
            MinimizeImage = MinimizeButton;

            // TextBoxes
            URLTextBox = urlTextBox;
            statusTextBox = StatusTextBox;

            // Loading part
            LoadSettingsFromFile();
            Loader.CheckForThemes();
            Loader.LoadMusicFiles();
            Loader.GetIpAddress();
            Loader.Load();
        }

        private bool isFirstTimeOpened = true;
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isFirstTimeOpened)
            {
                DoubleAnimation animation = new(0, 1, new Duration(TimeSpan.FromSeconds(1)))
                {
                    EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut }
                };
                this.BeginAnimation(Window.OpacityProperty, animation);

                // Wait for 3 seconds before setting the visibility of the grid to Hidden
                await Task.Delay(1000);

                DoubleAnimation opacityAnimation = new()
                {
                    From = 1,
                    To = 0,
                    Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                };
                OpeningAnim.BeginAnimation(Image.OpacityProperty, opacityAnimation);

                await Task.Delay(500);
                // Handle the Completed event to hide the grid when the opacity animation is finished
                OpeningAnim.Visibility = Visibility.Collapsed;

                isFirstTimeOpened = false;
            }
        }

        private void SideBarTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThemesManager.TabItemsSelectionChanged();
        }


        // Settings section
        private void CheckBoxLightMode_Checked(object sender, RoutedEventArgs e)
        {
            ThemesManager.LightModeChecked();
        }

        private void CheckBoxDarkMode_Checked(object sender, RoutedEventArgs e)
        {
            ThemesManager.DarkModeChecked();
        }

        private void AddMusic_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayer.AddMusic();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Image playButton = (Image)sender;
            MusicItem musicItem = (MusicItem)playButton.DataContext;
            string filePath = musicItem.FilePath;
            MusicPlayer.PlayMusic(filePath);
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayer.PauseMusic();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayer.StopMusic();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Image deleteButton = (Image)sender;
            MusicItem musicItem = (MusicItem)deleteButton.DataContext;
            string filePath = musicItem.FilePath;

            // Delete the file
            File.Delete(filePath);

            // Remove the item from the MusicItems list
            MusicPlayer.DeleteMusic(musicItem);
        }

        private void urlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string videoUrl = URLTextBox!.Text;
                URLTextBox!.Clear();
                MusicPlayer.DownloadMusic(videoUrl);
            }
        }
        private void JoinDiscordServer_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://discord.gg/ScR9MGbRSY");
        }

        // close the program
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // minimize the program
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private async void TemporaryFiles_Click(object sender, RoutedEventArgs e)
        {
            await Cleaner.DeleteTemporaryFilesAsync();
        }

        private void CleanupUtility_Click(object sender, RoutedEventArgs e)
        {
            Cleaner.CleanupUtility();
        }

        private void DRP_Click(object sender, RoutedEventArgs e)
        {
            if (DRP.IsChecked == true)
            {
                DiscordOptions.Visibility = Visibility.Visible;
                ConfigManager.SetDiscordRPC(true); // set DiscordRPC to true
                if (!DiscordRichPresenceManager.DiscordClient.IsInitialized)
                {
                    DiscordRichPresenceManager.DiscordClient.Initialize();
                }
                DiscordRichPresenceManager.SetPresence();
            }
            else
            {
                DiscordOptions.Visibility = Visibility.Collapsed;
                ConfigManager.SetDiscordRPC(false); // set DiscordRPC to false
                DiscordRichPresenceManager.DiscordClient.ClearPresence();
            }
        }

        private void StatusTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Get the text value from the TextBox
                string text = StatusTextBox!.Text;

                // Validate the entered text
                if (!string.IsNullOrEmpty(text))
                {
                    bool DiscordRPCisChecked = ConfigManager.GetDiscordRPC();
                    if (DiscordRPCisChecked == true)
                    {
                        DiscordRichPresenceManager.UpdatePresence(text);
                        ConfigManager.SetDiscordStatus(text);
                    }
                }
            }
            catch (Exception ex)
            {
                StarZMessageBox.ShowDialog($"Error updating Discord status: {ex.Message}", "Error", false);
            }
        }

        // Load the default values of the checkboxes, drop downs... on window's loading
        private void LoadSettingsFromFile()
        {
            bool DiscordRPCisChecked = ConfigManager.GetDiscordRPC();
            if (DiscordRPCisChecked == true)
            {
                DRP.IsChecked = true;
                DiscordOptions.Visibility = Visibility.Visible;
                string DiscordStatusText = ConfigManager.GetDiscordStatus();
                StatusTextBox!.Text = DiscordStatusText;
            }
            else
            {
                DRP.IsChecked = false;
                DiscordOptions.Visibility = Visibility.Collapsed;
            }
        }
    }
}
