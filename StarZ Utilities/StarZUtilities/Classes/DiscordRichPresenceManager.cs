using DiscordRPC;


namespace StarZUtilities.Classes
{
    //From StarZ Launcher
    public static class DiscordRichPresenceManager
    {
        private static readonly string ClientId = "1262941314006122567";
        private static DiscordRpcClient? discordClient;

        public static DiscordRpcClient DiscordClient
        {
            get
            {
                discordClient ??= new DiscordRpcClient(ClientId);
                return discordClient;
            }
        }

        public static void SetPresence()
        {
            DiscordClient.SetPresence(new RichPresence
            {
                State = "Using StarZ Utilities",
                Timestamps = Timestamps.Now,
                Assets = new Assets
                {
                    LargeImageKey = "starz",
                    LargeImageText = "StarZ Utilities",
                }
            });
        }


        public static void UpdatePresence(string status)
        {
            DiscordClient.UpdateState(status);
        }

        public static void TerminatePresence()
        {
            if (discordClient == null || discordClient.IsDisposed) return;
            discordClient.ClearPresence();
            discordClient.Deinitialize();
            discordClient.Dispose();
        }
    }
}