using System;

namespace MSCalc.Dialog
{
    public interface IDialogView
    {
        void SetMessage(string message);
        void SetVisible(bool visible);
        event Action OnClosed;
    }
}