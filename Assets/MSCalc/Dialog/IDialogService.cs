using System;

namespace MSCalc.Dialog
{
    public interface IDialogService
    {
        void ShowError(string message);

        event Action OnShown;
        event Action OnClosed;
    }
}