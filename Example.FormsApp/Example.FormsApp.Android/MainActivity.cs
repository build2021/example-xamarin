namespace Example.FormsApp.Droid
{
    using System.Threading.Tasks;

    using Acr.UserDialogs;

    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;

    using Example.FormsApp.Components.Device;
    using Example.FormsApp.Components.Dialog;
    using Example.FormsApp.Droid.Components.Device;
    using Example.FormsApp.Droid.Components.Dialog;
    using Example.FormsApp.Helpers;

    using Smart.Forms.Resolver;
    using Smart.Resolver;

    [Activity(
        Name = "example.app.MainActivity",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme.Splash",
        MainLauncher = true,
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize,
        ScreenOrientation = ScreenOrientation.Portrait,
        WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private DeviceManager deviceManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState);

            // Setup crash report
            //AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            //    CrashReportHelper.LogException(args.ExceptionObject as Exception);
            TaskScheduler.UnobservedTaskException += (_, args) =>
                CrashReportHelper.LogException(args.Exception);
            AndroidEnvironment.UnhandledExceptionRaiser += (_, args) =>
                CrashReportHelper.LogException(args.Exception);

            // Database
            SQLitePCL.Batteries_V2.Init();

            // Service
            deviceManager = new DeviceManager(this);

            // Components
            UserDialogs.Init(this);
            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Boot
            LoadApplication(new App(new ComponentProvider(this)));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }

        private sealed class ComponentProvider : IComponentProvider
        {
            private readonly MainActivity activity;

            public ComponentProvider(MainActivity activity)
            {
                this.activity = activity;
            }

            public void RegisterComponents(ResolverConfig config)
            {
                config.Bind<Context>().ToConstant(activity).InSingletonScope();

                config.Bind<IApplicationDialog>().To<ApplicationDialog>().InSingletonScope();
                config.Bind<IDeviceManager>().ToConstant(activity.deviceManager).InSingletonScope();
            }
        }
    }
}
