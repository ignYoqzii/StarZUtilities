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
        public static void LoadTheme(string theme)
        {
            // Construct the URI to the resource dictionary in the Themes folder
            var uri = new Uri("/Themes/" + theme, UriKind.RelativeOrAbsolute);

            // Load the resource dictionary
            ResourceDictionary resourceDict = new();
            resourceDict.Source = uri;

            // Clear existing merged dictionaries and add the new one
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        public static void DarkModeChecked()
        {
            LoadTheme("DarkMode.xaml");
            LightModeCheckBox!.IsChecked = false;
            LightModeCheckBox!.IsEnabled = true;
            DarkModeCheckBox!.IsEnabled = false;
            ConfigManager.SetTheme("Dark");
        }

        public static void LightModeChecked()
        {
            LoadTheme("LightMode.xaml");
            DarkModeCheckBox!.IsChecked = false;
            DarkModeCheckBox!.IsEnabled = true;
            LightModeCheckBox!.IsEnabled = false;
            ConfigManager.SetTheme("Light");
        }
    }
}
