namespace Example.FormsApp.State
{
    using Xamarin.Essentials;

    public class Configuration
    {
        public string ServerAddress
        {
#if  DEBUG
            get => Preferences.Get(nameof(ServerAddress), "https://10.13.8.25:30443/");
#else
            get => Preferences.Get(nameof(ServerAddress), "https://xxxx/");
#endif
            set => Preferences.Set(nameof(ServerAddress), value);
        }
    }
}
