using StarZUtilities.Windows;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

public static class Cleaner
{
    public static async Task DeleteTemporaryFilesAsync()
    {

        if (!IsAdministrator())
        {
            // Restart the program and request elevated rights
            StarZMessageBox.ShowDialog("You do not have administrator privileges. Please close the program, right click on it and select 'Run as administrator'.", "Error", false);
            return;
        }

        string logFileName = $"TempFiles {DateTime.Now:yyyy-MM-dd_HHmmss}.txt";
        string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StarZ Utilities", "Logs", logFileName);

        // Log the start of the cleaning process
        Log($"Cleaning process started at: {DateTime.Now}", logFilePath);

        try
        {
            await Task.Run(() =>
            {
                // Get user's temporary folder
                string userTempFolderPath = Path.GetTempPath();
                DirectoryInfo userTempDirectory = new(userTempFolderPath);

                // Log the operation of deleting files in user's temporary folder
                Log($"Deleting files in user's temporary folder: {userTempFolderPath}", logFilePath);

                // Delete files in user's temporary folder
                foreach (FileInfo file in userTempDirectory.GetFiles())
                {
                    try
                    {
                        file.Delete();
                        Log($"Deleted file: {file.FullName}", logFilePath);
                    }
                    catch (IOException ex)
                    {
                        Log($"Error deleting file {file.FullName}: {ex.Message}", logFilePath);
                        // Handle specific exception for file in use
                    }
                    catch (Exception ex)
                    {
                        Log($"Error deleting file {file.FullName}: {ex.Message}", logFilePath);
                        // Handle other exceptions
                    }
                }

                // Log the operation of deleting directories in user's temporary folder
                Log($"Deleting directories in user's temporary folder: {userTempFolderPath}", logFilePath);

                // Delete directories in user's temporary folder
                foreach (DirectoryInfo dir in userTempDirectory.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true); // Recursive delete
                        Log($"Deleted directory: {dir.FullName}", logFilePath);
                    }
                    catch (IOException ex)
                    {
                        Log($"Error deleting directory {dir.FullName}: {ex.Message}", logFilePath);
                        // Handle specific exception for directory in use
                    }
                    catch (Exception ex)
                    {
                        Log($"Error deleting directory {dir.FullName}: {ex.Message}", logFilePath);
                        // Handle other exceptions
                    }
                }

                // Get system-wide temporary folder
                string systemTempFolderPath = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "Temp");
                DirectoryInfo systemTempDirectory = new(systemTempFolderPath);

                // Log the operation of deleting files in system-wide temporary folder
                Log($"Deleting files in system-wide temporary folder: {systemTempFolderPath}", logFilePath);

                // Delete files in system-wide temporary folder
                foreach (FileInfo file in systemTempDirectory.GetFiles())
                {
                    try
                    {
                        file.Delete();
                        Log($"Deleted file: {file.FullName}", logFilePath);
                    }
                    catch (IOException ex)
                    {
                        Log($"Error deleting file {file.FullName}: {ex.Message}", logFilePath);
                        // Handle specific exception for file in use
                    }
                    catch (Exception ex)
                    {
                        Log($"Error deleting file {file.FullName}: {ex.Message}", logFilePath);
                        // Handle other exceptions
                    }
                }

                // Log the operation of deleting directories in system-wide temporary folder
                Log($"Deleting directories in system-wide temporary folder: {systemTempFolderPath}", logFilePath);

                // Delete directories in system-wide temporary folder
                foreach (DirectoryInfo dir in systemTempDirectory.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true); // Recursive delete
                        Log($"Deleted directory: {dir.FullName}", logFilePath);
                    }
                    catch (IOException ex)
                    {
                        Log($"Error deleting directory {dir.FullName}: {ex.Message}", logFilePath);
                        // Handle specific exception for directory in use
                    }
                    catch (Exception ex)
                    {
                        Log($"Error deleting directory {dir.FullName}: {ex.Message}", logFilePath);
                        // Handle other exceptions
                    }
                }
            });

            // Log success message
            Log($"Temporary files deleted successfully.", logFilePath);
            StarZMessageBox.ShowDialog("Temporary files deleted successfully.", "Success", false);
        }
        catch (Exception ex)
        {
            // Log error message
            Log($"Error deleting temporary files: {ex.Message}", logFilePath);
            StarZMessageBox.ShowDialog($"Error deleting temporary files: {ex.Message}", "Error", false);
        }
    }

    public static void CleanupUtility()
    {
        try
        {
            Process.Start("cleanmgr.exe");
        }
        catch (Exception ex)
        {
            StarZMessageBox.ShowDialog($"Failed to open Windows Disk Cleanup utility: {ex.Message}", "Error", false);
        }
    }

    private static void Log(string message, string logFilePath)
    {
        try
        {
            File.AppendAllText(logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            StarZMessageBox.ShowDialog($"Failed to write to log file: {ex.Message}", "Error", false);
        }
    }

    private static bool IsAdministrator()
    {
        var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
        var principal = new System.Security.Principal.WindowsPrincipal(identity);
        return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
    }
}



