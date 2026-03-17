using System;
using System.Collections.Generic;
using System.Text;

namespace MSCalc.Calculator
{
    public class CalcModel : ICalcModel
    {
        private List<string> _history = new List<string>();
        public List<string> History => _history;
        
        private string _currentInput = string.Empty;
        public string CurrentInput => _currentInput;

        public event Action<CalculationResult> OnCalculationFinished;
        public event Action<string, List<string>> OnStateRestored;
        public void UpdateInput(string input)
        {
            _currentInput = input;
        }

        public void RestoreState(string input, List<string> history)
        {
            _currentInput = input ?? string.Empty;
            _history = history != null ? new List<string>(history) : new List<string>();
            OnStateRestored?.Invoke(_currentInput, _history);
        }

        public void Calculate()
        {
            string result = string.Empty;
            bool isError = false;

            string[] parts = _currentInput.Split('+');

            if (
                parts.Length == 2
                && uint.TryParse(parts[0], out uint a)
                && uint.TryParse(parts[1], out uint b)
            )
            {
                result = (a + b).ToString();
            }
            else
            {
                result = "Error";
                isError = true;
            }
            RegisterHistoryRecord(_currentInput, result);

            CalculationResult calculationResult = new CalculationResult(_history, isError);
            OnCalculationFinished?.Invoke(calculationResult);
        }

        public void RegisterHistoryRecord(string input, string result)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(input);
            stringBuilder.Append("=");
            stringBuilder.Append(result);

            _history.Add(stringBuilder.ToString());
        }
    }
}
