namespace Example.FormsApp.Modules
{
    using System.Threading.Tasks;

    using Example.FormsApp.Shell;

    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class AppViewModelBase : ViewModelBase, INavigatorAware, INavigationEventSupport, INotifySupportAsync<ShellEvent>, INotifySupportAsync<string>
    {
        public INavigator Navigator { get; set; }

        public ApplicationState ApplicationState { get; }

        protected AppViewModelBase(ApplicationState applicationState)
            : base(applicationState)
        {
            ApplicationState = applicationState;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            System.Diagnostics.Debug.WriteLine($"{GetType()} is Disposed");
        }

        public virtual void OnNavigatingFrom(INavigationContext context)
        {
        }

        public virtual void OnNavigatingTo(INavigationContext context)
        {
        }

        public virtual void OnNavigatedTo(INavigationContext context)
        {
        }

        public Task NavigatorNotifyAsync(ShellEvent parameter)
        {
            switch (parameter)
            {
                case ShellEvent.Back:
                    return OnNotifyBackAsync();
                default:
                    return Task.CompletedTask;
            }
        }

        public async Task NavigatorNotifyAsync(string parameter)
        {
            if (BusyState.IsBusy)
            {
                return;
            }

            await BusyState.UsingAsync(async () => await OnScan(parameter));
        }

        protected virtual Task OnScan(string input)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnNotifyBackAsync()
        {
            return Task.CompletedTask;
        }
    }
}
