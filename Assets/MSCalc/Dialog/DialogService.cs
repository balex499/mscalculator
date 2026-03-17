using System;

namespace MSCalc.Dialog
{
    public class DialogService : IDialogService, IDisposable
    {
        private readonly IDialogView _view;

        public event Action OnShown;
        public event Action OnClosed;

        public DialogService(IDialogView view)
        {
            _view = view;
            _view.OnClosed += HandleClosed;
        }

        public void ShowError(string message)
        {
            _view.SetMessage(message);
            _view.SetVisible(true);

            OnShown?.Invoke();
        }

        private void HandleClosed()
        {
            _view.SetVisible(false);
            OnClosed?.Invoke();
        }

        public void Dispose()
        {
            _view.OnClosed -= HandleClosed;
        }
    }
}