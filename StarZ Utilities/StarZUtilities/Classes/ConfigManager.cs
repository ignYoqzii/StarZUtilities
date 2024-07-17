using System;
using System.Collections.Generic;
using System.IO;

namespace StarZUtilities.Classes
{
    // This class handles the Config.txt file (Taken from StarZ Launcher)
    public static class ConfigManager
    {
        // Default values for settings
        private const string THEME = "Light";

        private static readonly string configFilePath;

        // Settings and their values
        private static readonly Dictionary<string, object> settings = new();

        static ConfigManager()
        {
            // Set default values for settings
            settings.Add("Theme", THEME);

            // Get the config file path in MyDocuments/StarZ Client
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            configFilePath = Path.Combine(myDocumentsPath, "StarZ Utilities", "Config.txt");

            // Create the config file if it doesn't already exist
            if (!File.Exists(configFilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(configFilePath));
                File.Create(configFilePath).Close();
                WriteDefaultSettingsToFile();
            }

            // Wait for a short time to give the file creation process a chance to complete
            int maxAttempts = 10;
            int attempt = 0;
            while (!File.Exists(configFilePath) && attempt < maxAttempts)
            {
                attempt++;
                System.Threading.Thread.Sleep(100);
            }

            // Load settings from the config file
            LoadSettings();
        }

        private static void WriteDefaultSettingsToFile()
        {
            using StreamWriter writer = new(configFilePath);
            foreach (KeyValuePair<string, object> kvp in settings)
            {
                writer.WriteLine($"{kvp.Key} = {kvp.Value}");
            }
        }

        private static void LoadSettings()
        {
            // Read the config file line by line
            using StreamReader reader = new(configFilePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Split each line into key and value
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    // Trim whitespace from key and value
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    // Convert value to correct type and add to settings
                    if (settings.ContainsKey(key))
                    {
                        if (settings[key] is bool)
                        {
                            settings[key] = bool.Parse(value);
                        }
                        else if (settings[key] is string)
                        {
                            settings[key] = value;
                        }
                    }
                }
            }
        }

        public static string GetTheme()
        {
            return (string)settings["Theme"];
        }

        public static void SetTheme(string newTheme)
        {
            // Update the value in the settings dictionary
            settings["Theme"] = newTheme;

            // Write the updated settings to the config file
            using StreamWriter writer = new(configFilePath);
            foreach (KeyValuePair<string, object> kvp in settings)
            {
                writer.WriteLine($"{kvp.Key} = {kvp.Value}");
            }
        }
    }
}

