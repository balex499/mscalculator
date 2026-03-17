using System.Collections.Generic;
using System.Text;
using MSCalc.Save;
using MSCalc.Dialog;
using System;

namespace MSCalc.Calculator
{
    public class CalcPresenter : IDisposable
    {
        private readonly ICalcView _view;
        private readonly ICalcModel _model;

        private readonly ISaveSystem _saveSystem;
        private readonly IDialogService _dialogs;

        private readonly Action _onDialogShown;
        private readonly Action _onDialogClosed;

        public CalcPresenter(
            ICalcModel model,
            ICalcView view,
            ISaveSystem saveSystem = null,
            IDialogService dialogs = null)
        {
            _model = model;
            _model.OnCalculationFinished += HandleResult;
            _model.OnStateRestored += HandleStateRestore;

            _view = view;
            _view.OnResultRequested += HandleResultRequested;
            _view.OnInputChanged += HandleInputChanged;

            _saveSystem = saveSystem;
            _dialogs = dialogs;

            _onDialogShown = () => _view.SetVisible(false);
            _onDialogClosed = () => _view.SetVisible(true);

            if (_dialogs != null)
            {
                _dialogs.OnShown += _onDialogShown;
                _dialogs.OnClosed += _onDialogClosed;
            }

            RestoreSavedState();
        }

        private void RestoreSavedState()
        {
            if (_saveSystem == null)
                return;

            var saved = _saveSystem.LoadState();
            if (saved == null)
                return;

            _model.RestoreState(saved.Input, saved.History);

        }

        private void HandleResultRequested()
        {
            _model.Calculate();
        }

        private void HandleInputChanged(string input)
        {
            _model.UpdateInput(input);
            SaveCurrentState(input);
        }

        private void HandleResult(CalculationResult result)
        {

            _view.SetHistory(BuildReversedHistory(result.history), result.history.Count);

            if (result.isError)
            {
                _dialogs?.ShowError("Please check the expression you just entered");
                SaveCurrentState(_view.GetActiveInput());
                return;
            }

            _view.ClearInput();
            SaveCurrentState(string.Empty);
        }

        private void HandleStateRestore(string input, List<string> history)
        {

            _view.SetHistory(BuildReversedHistory(history), history.Count);
            _view.ClearInput();
        }

        private string BuildReversedHistory(List<string> history)
        {
            if (history == null || history.Count == 0)
                return string.Empty;

            var stringBuilder = new StringBuilder();
            for (int i = history.Count - 1; i >= 0; i--)
            {
                stringBuilder.Append(history[i]);
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }

        private void SaveCurrentState(string input)
        {
            if (_saveSystem == null) return;

            _saveSystem.SaveState(_model.CurrentInput, _model.History);
        }

        public void Dispose()
        {
            _model.OnCalculationFinished -= HandleResult;
            _view.OnResultRequested -= HandleResultRequested;
            _view.OnInputChanged -= HandleInputChanged;

            if (_dialogs != null)
            {
                _dialogs.OnShown -= _onDialogShown;
                _dialogs.OnClosed -= _onDialogClosed;
            }

            SaveCurrentState(_view.GetActiveInput());
        }
    }
}

