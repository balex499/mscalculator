using System;
using System.Collections.Generic;
using System.Text;

namespace MSCalc.Calculator
{
    public interface ICalcModel
    {
        public List<string> History { get; }
        public string CurrentInput { get; }
        void UpdateInput(string input);
        void Calculate();
        void RegisterHistoryRecord(string input, string result);

        void RestoreState(string input, List<string> history);

        event Action<CalculationResult> OnCalculationFinished;
        event Action<string, List<string>> OnStateRestored;
    }

    public class CalculationResult
    {
        public List<string> history = new List<string>();
        public bool isError = false;

        public CalculationResult(List<string> history, bool isError)
        {
            this.history = history;
            this.isError = isError;
        }
    }
}
