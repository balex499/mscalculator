using System;

namespace MSCalc.Calculator
{
    public interface ICalcView
    {
        string GetActiveInput();
        void ClearInput();
        void SetInput(string input);

        void SetVisible(bool visible);

        void SetHistory(string history, int recordsCount);
        void ClearHistory();

        event Action OnResultRequested;
        event Action<string> OnInputChanged;
    }
}