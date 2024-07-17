using Microsoft.Win32;
using StarZUtilities.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using YoutubeExplode;

namespace StarZUtilities.Classes
{
    public static class MusicPlayer
    {
        public static string musicDirectoryPath;
        public static MediaPlayer mediaPlayer;
        public static bool isPaused = false; // Track whether the music is paused or not
        public static TimeSpan currentPosition = TimeSpan.Zero; // Track the current position of the media player

        public static ObservableCollection<MusicItem> MusicItems { get; set; } = new ObservableCollection<MusicItem>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        static MusicPlayer()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            musicDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StarZ Utilities", "Musics");
            Directory.CreateDirectory(musicDirectoryPath);
            mediaPlayer = new MediaPlayer();
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded; // Subscribe to the MediaEnded event
        }

        private static void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            StopMusic();
        }

        public static void AddMusic()
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Audio Files|*.mp3";

            if (openFileDialog.ShowDialog() == true)
            {

                foreach (string filePath in openFileDialog.FileNames)
                {
                    string destinationPath = Path.Combine(musicDirectoryPath, Path.GetFileName(filePath));
                    File.Copy(filePath, destinationPath, true);
                }

                Loader.LoadMusicFiles();
            }
        }

        public static void PlayMusic(string filepath)
        {
            if (isPaused)
            {
                mediaPlayer.Position = currentPosition; // Set the media player's position to the saved position
                mediaPlayer.Play();
                isPaused = false;
            }
            else
            {
                mediaPlayer.Open(new Uri(filepath));
                mediaPlayer.Play();
            }
        }

        public static void PauseMusic()
        {
            mediaPlayer.Pause();
            currentPosition = mediaPlayer.Position; // Save the current position
            isPaused = true;
        }

        public static void StopMusic()
        {
            mediaPlayer.Stop();
            mediaPlayer.Close();
            isPaused = false;
            currentPosition = TimeSpan.Zero; // Reset the current position
        }

        public static void DeleteMusic(MusicItem musicItem)
        {
            MusicItems.Remove(musicItem);
            Loader.LoadMusicFiles();
        }

        public static event PropertyChangedEventHandler PropertyChanged;

        private static void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        public static async void DownloadMusic(string videoUrl)
        {
            try
            {
                var youtube = new YoutubeClient();
                var video = await youtube.Videos.GetAsync(videoUrl);

                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                var audioStreamInfo = streamManifest.GetAudioStreams().FirstOrDefault();

                if (audioStreamInfo != null)
                {
                    string sanitizedTitle = SanitizeFileName(video.Title);
                    string outputFilePath = Path.Combine(musicDirectoryPath, $"{sanitizedTitle}.mp3");

                    await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, outputFilePath);
                    Loader.LoadMusicFiles();
                }
                else
                {
                    MessageBox.Show("No audio stream found for the provided video URL.");
                }
            }
            catch (Exception ex)
            {
                StarZMessageBox.ShowDialog($"An error occurred: {ex.Message}", "Error !", false);
            }
        }

        private static string SanitizeFileName(string fileName)
        {
            string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (char invalidChar in invalidChars)
            {
                fileName = fileName.Replace(invalidChar.ToString(), "");
            }
            return fileName;
        }
    }

    public class MusicItem
    {
        public string FilePath { get; }
        public string Title { get; }
        public string Artist { get; }
        public ImageSource Image { get; }

        public MusicItem(string filePath)
        {
            FilePath = filePath;
            Title = Path.GetFileNameWithoutExtension(filePath);
            Artist = GetArtistFromAudio(filePath);
            Image = LoadImageFromAudio(filePath);
        }

        private ImageSource LoadImageFromAudio(string filePath)
        {

            var defaultImage = new BitmapImage(new Uri("/Resources/Unknow.png", UriKind.Relative));
            try
            {
                TagLib.File file = TagLib.File.Create(filePath);
                TagLib.IPicture picture = file.Tag.Pictures.FirstOrDefault();
                if (picture != null)
                {
                    MemoryStream memoryStream = new(picture.Data.Data);
                    BitmapImage bitmapImage = new();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    memoryStream.Dispose();
                    return bitmapImage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading image from audio file: " + ex.Message);
            }

            return defaultImage;
        }

        private static string GetArtistFromAudio(string filePath)
        {
            try
            {
                TagLib.File file = TagLib.File.Create(filePath);
                string artist = file.Tag.FirstPerformer;

                // Check if the artist information is blank or empty
                if (string.IsNullOrEmpty(artist))
                {
                    return "Unknown";
                }

                return artist;
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}


