using UnityEngine;
using MSCalc.Calculator;
using MSCalc.Dialog;
using MSCalc.Save;

namespace MSCalc.Calculator
{
    public class CalcBootstrap : MonoBehaviour
    {
        [SerializeField]
        private CalcView _calcView;
        [SerializeField]
        private DialogView _dialogView;
        private CalcPresenter _calcPresenter;
        private DialogService _dialogService;
        private void Awake()
        {
            CalcModel model = new CalcModel();
            var saveSystem = new PlayerPrefsSaveSystem();

            _dialogService = new DialogService(_dialogView);

            _calcPresenter = new CalcPresenter(model, _calcView, saveSystem, _dialogService);
        }

        private void OnDestroy()
        {
            _calcPresenter?.Dispose();
            _dialogService?.Dispose();
        }
    }
}
