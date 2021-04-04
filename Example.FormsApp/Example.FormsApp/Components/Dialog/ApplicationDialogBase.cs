namespace Example.FormsApp.Components.Dialog
{
    using System.Threading.Tasks;

    using XamarinFormsComponents.Dialogs;

    public abstract class ApplicationDialogBase : IApplicationDialog
    {
        private readonly IDialogs dialogs;

        protected ApplicationDialogBase(IDialogs dialogs)
        {
            this.dialogs = dialogs;
        }

        public ValueTask<bool> Confirm(string message)
        {
            return Confirm(string.Empty, message, "はい", "いいえ");
        }

        public abstract ValueTask<bool> Confirm(string title, string message, string ok, string cancel);

        public ValueTask Information(string message)
        {
            return Information(string.Empty, message, "OK");
        }

        public abstract ValueTask Information(string title, string message, string ok);

        public IProgress Progress(string title)
        {
            return dialogs.Progress(title);
        }

        public IProgress Loading(string title)
        {
            return dialogs.Loading(title);
        }
    }
}
