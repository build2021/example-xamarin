namespace Example.FormsApp.Components.Dialog
{
    using System.Threading.Tasks;

    using XamarinFormsComponents.Dialogs;

    public interface IApplicationDialog
    {
        ValueTask<bool> Confirm(string message);

        ValueTask<bool> Confirm(string title, string message, string ok, string cancel);

        ValueTask Information(string message);

        ValueTask Information(string title, string message, string ok);

        IProgress Progress(string title);

        IProgress Loading(string title);
    }
}
