using StarZUtilities.Classes;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public static Border? WindowBackground;
        public static Border? BackgroundForWindowsOnTop;

        public static CheckBox? DarkModeCheckBox;
        public static CheckBox? LightModeCheckBox;

        public static TextBlock? MusicPlayerInformationTextBlock;
        public static TextBlock? discordUsernameTextBlock;
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

        public static TextBox? URLTextBox;
        public static TextBox? VideoURLTextBox;

        public static Image? CloseImage;
        public static Image? MinimizeImage;

        public static List<Border?> Backgrounds = new();
        public static List<TextBlock?> TextBlocks = new();

        private HardwareMonitor hardwareMonitor;

        public MainWindow()
        {
            Loader.CreateDirectory();
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
            WindowBackground = MenuMainBackground;
            BackgroundForWindowsOnTop = DarkBackgroundWindows;

            // Themes checkboxes
            DarkModeCheckBox = CheckBoxDarkMode;
            LightModeCheckBox = CheckBoxLightMode;

            // Individual TextBlocks
            MusicPlayerInformationTextBlock = MusicPlayerInfoTextBlock;
            discordUsernameTextBlock = DiscordUsernameTextBlock;
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

            // Buttons
            CloseImage = CloseButton;
            MinimizeImage = MinimizeButton;

            // TextBoxes
            URLTextBox = urlTextBox;

            // Add items to the lists
            Backgrounds.Add(SideBarBackground);
            Backgrounds.Add(TopBarBackground);
            Backgrounds.Add(TopBarBackground2);
            Backgrounds.Add(MainBackground);
            Backgrounds.Add(LogoBackground);
            Backgrounds.Add(ThemesBackground);
            Backgrounds.Add(ColorsBackground);
            Backgrounds.Add(HomeBackground1);
            Backgrounds.Add(HomeBackground2);
            Backgrounds.Add(HomeBackground3);
            Backgrounds.Add(URLBackground);
            Backgrounds.Add(AboutCPUBackground);
            Backgrounds.Add(AboutGPUBackground);
            Backgrounds.Add(AboutFanBackground);
            Backgrounds.Add(AboutOthersBackground);
            Backgrounds.Add(DiskCleanerBackground);
            Backgrounds.Add(RegistryCleanerBackground);
            Backgrounds.Add(TracksEraserBackground);
            Backgrounds.Add(SomethingBackground);

            TextBlocks.Add(VersionText);
            TextBlocks.Add(HomeTextBlock);
            TextBlocks.Add(CleanTextBlock);
            TextBlocks.Add(AboutTextBlock);
            TextBlocks.Add(MusicPlayerTextBlock);
            TextBlocks.Add(SettingsTextBlock);
            TextBlocks.Add(ThemesTextBlock);
            TextBlocks.Add(GeneralTextBlock);
            TextBlocks.Add(MemoryTextBlock);
            TextBlocks.Add(MotherboardTextBlock);
            TextBlocks.Add(IPAddressTextBlock);
            TextBlocks.Add(MusicPlayerInfoTextBlock);
            TextBlocks.Add(URLTextBlock);
            TextBlocks.Add(CPUTextBlock);
            TextBlocks.Add(GPUTextBlock);
            TextBlocks.Add(CPUTempTextBlock);
            TextBlocks.Add(GPUTempTextBlock);
            TextBlocks.Add(CPUNameTextBlock);
            TextBlocks.Add(CPULoadTextBlock);
            TextBlocks.Add(GPUNameTextBlock);
            TextBlocks.Add(GPULoadTextBlock);
            TextBlocks.Add(FansTextBlock);
            TextBlocks.Add(OthersTextBlock);
            TextBlocks.Add(CPUFanTextBlock);
            TextBlocks.Add(GPUFanTextBlock);
            TextBlocks.Add(WelcomeTextBlock);
            TextBlocks.Add(DiscordServerTextBlock);
            TextBlocks.Add(DiscordServerTextBlock2);
            TextBlocks.Add(ChangelogTextBlock);
            TextBlocks.Add(ChangelogTextBlock2);
            TextBlocks.Add(DiscordUsernameTextBlock);
            TextBlocks.Add(DiskCleanerTextBlock);
            TextBlocks.Add(RegistryCleanerTextBlock);
            TextBlocks.Add(TracksEraserTextBlock);
            TextBlocks.Add(SomethingTextBlock);

            // Loading part
            Loader.CheckForThemes();
            Loader.LoadMusicFiles();
            Loader.GetIpAddress();
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
    }
}
