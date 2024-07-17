using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static StarZUtilities.Windows.MainWindow;

namespace StarZUtilities.Classes
{
    public static class ThemesManager
    {
        public static void TabItemsSelectionChanged()
        {
            TabItem? SelectedTabItem = MainTabControl!.SelectedItem as TabItem;

            if (SelectedTabItem == HometabItem)
            {
                HomeTabImage!.Source = new BitmapImage(new Uri("/Resources/HomeWhite.png", UriKind.Relative));
                CleanTabImage!.Source = new BitmapImage(new Uri("/Resources/CleanGray.png", UriKind.Relative));
                AboutTabImage!.Source = new BitmapImage(new Uri("/Resources/InfoGray.png", UriKind.Relative));
                MusicPlayerTabImage!.Source = new BitmapImage(new Uri("/Resources/MusicPlayerGray.png", UriKind.Relative));
                SettingsTabImage!.Source = new BitmapImage(new Uri("/Resources/SettingsGray.png", UriKind.Relative));
            }
            else if (SelectedTabItem == CleantabItem)
            {
                HomeTabImage!.Source = new BitmapImage(new Uri("/Resources/HomeGray.png", UriKind.Relative));
                CleanTabImage!.Source = new BitmapImage(new Uri("/Resources/Clean.png", UriKind.Relative));
                AboutTabImage!.Source = new BitmapImage(new Uri("/Resources/InfoGray.png", UriKind.Relative));
                MusicPlayerTabImage!.Source = new BitmapImage(new Uri("/Resources/MusicPlayerGray.png", UriKind.Relative));
                SettingsTabImage!.Source = new BitmapImage(new Uri("/Resources/SettingsGray.png", UriKind.Relative));
            }
            else if (SelectedTabItem == AbouttabItem)
            {
                HomeTabImage!.Source = new BitmapImage(new Uri("/Resources/HomeGray.png", UriKind.Relative));
                CleanTabImage!.Source = new BitmapImage(new Uri("/Resources/CleanGray.png", UriKind.Relative));
                AboutTabImage!.Source = new BitmapImage(new Uri("/Resources/Info.png", UriKind.Relative));
                MusicPlayerTabImage!.Source = new BitmapImage(new Uri("/Resources/MusicPlayerGray.png", UriKind.Relative));
                SettingsTabImage!.Source = new BitmapImage(new Uri("/Resources/SettingsGray.png", UriKind.Relative));
            }
            else if (SelectedTabItem == MusicPlayertabItem)
            {
                HomeTabImage!.Source = new BitmapImage(new Uri("/Resources/HomeGray.png", UriKind.Relative));
                CleanTabImage!.Source = new BitmapImage(new Uri("/Resources/CleanGray.png", UriKind.Relative));
                AboutTabImage!.Source = new BitmapImage(new Uri("/Resources/InfoGray.png", UriKind.Relative));
                MusicPlayerTabImage!.Source = new BitmapImage(new Uri("/Resources/MusicPlayerWhite.png", UriKind.Relative));
                SettingsTabImage!.Source = new BitmapImage(new Uri("/Resources/SettingsGray.png", UriKind.Relative));
            }
            else if (SelectedTabItem == SettingstabItem)
            {
                HomeTabImage!.Source = new BitmapImage(new Uri("/Resources/HomeGray.png", UriKind.Relative));
                CleanTabImage!.Source = new BitmapImage(new Uri("/Resources/CleanGray.png", UriKind.Relative));
                AboutTabImage!.Source = new BitmapImage(new Uri("/Resources/InfoGray.png", UriKind.Relative));
                MusicPlayerTabImage!.Source = new BitmapImage(new Uri("/Resources/MusicPlayerGray.png", UriKind.Relative));
                SettingsTabImage!.Source = new BitmapImage(new Uri("/Resources/SettingsWhite.png", UriKind.Relative));
            }
        }

        public static void LoadLightMode()
        {
            // Backgrounds
            WindowBackground!.Background = (SolidColorBrush)Application.Current.Resources["LightMode1"];
            foreach (var background in Backgrounds)
            {
                if (background != null)
                {
                    background.Background = (SolidColorBrush)Application.Current.Resources["LightMode2"];
                }
            }

            // TextBoxes
            URLTextBox!.Foreground = (SolidColorBrush)Application.Current.Resources["LightTextColor1"];

            // Close and Minimize buttons
            CloseImage!.Source = new BitmapImage(new Uri("/Resources/CloseGray.png", UriKind.Relative));
            MinimizeImage!.Source = new BitmapImage(new Uri("/Resources/MinimizeGray.png", UriKind.Relative));

            // Logo
            StarZLogoImage!.Source = new BitmapImage(new Uri("/Resources/StarZLogoDark.png", UriKind.Relative));

            // TextBlocks
            foreach (var textblock in TextBlocks)
            {
                if (textblock != null)
                {
                    textblock.Foreground = (SolidColorBrush)Application.Current.Resources["LightTextColor1"];
                }
            }
        }

        public static void LoadDarkMode()
        {
            // Backgrounds
            WindowBackground!.Background = (SolidColorBrush)Application.Current.Resources["DarkMode1"];
            foreach (var background in Backgrounds)
            {
                if (background != null)
                {
                    background.Background = (SolidColorBrush)Application.Current.Resources["DarkMode2"];
                }
            }

            // TextBoxes
            URLTextBox!.Foreground = (SolidColorBrush)Application.Current.Resources["DarkTextColor1"];

            // Close and Minimize buttons
            CloseImage!.Source = new BitmapImage(new Uri("/Resources/CloseWhite.png", UriKind.Relative));
            MinimizeImage!.Source = new BitmapImage(new Uri("/Resources/MinimizeWhite.png", UriKind.Relative));

            // Logo
            StarZLogoImage!.Source = new BitmapImage(new Uri("/Resources/StarZLogoLight.png", UriKind.Relative));

            // TextBlocks
            foreach (var textblock in TextBlocks)
            {
                if (textblock != null)
                {
                    textblock.Foreground = (SolidColorBrush)Application.Current.Resources["DarkTextColor1"];
                }
            }
        }

        public static void DarkModeChecked()
        {
            LoadDarkMode();
            LightModeCheckBox!.IsChecked = false;
            LightModeCheckBox!.IsEnabled = true;
            DarkModeCheckBox!.IsEnabled = false;
            ConfigManager.SetTheme("Dark");
        }

        public static void LightModeChecked()
        {
            LoadLightMode();
            DarkModeCheckBox!.IsChecked = false;
            DarkModeCheckBox!.IsEnabled = true;
            LightModeCheckBox!.IsEnabled = false;
            ConfigManager.SetTheme("Light");
        }
    }
}
