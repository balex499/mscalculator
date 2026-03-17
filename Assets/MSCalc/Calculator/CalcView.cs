using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace MSCalc.Calculator
{
    public class CalcView : MonoBehaviour, ICalcView
    {
        [SerializeField]
        private TMPro.TMP_InputField _input;

        [SerializeField]
        private Button _resultButton;

        [SerializeField]
        private TMPro.TMP_Text _history;

        [SerializeField]
        private RectTransform _scrollView;

        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private float _defaultHistoryHeight;

        [SerializeField]
        private float _maxHistoryHeight;

        public event Action OnResultRequested;
        public event Action<string> OnInputChanged;

        private void Awake()
        {
            if (_input != null)
            {
                _input.onValueChanged.AddListener((value) => OnInputChanged?.Invoke(value));
            }
            if (_resultButton != null)
            {
                _resultButton.onClick.AddListener(() => OnResultRequested?.Invoke());
            }
            
        }

        public void SetHistory(string history, int recordsCount)
        {
            if (_history != null)
            {
                _history.text = history;
            }
            if (_scrollView != null)
            {
                float height = _history.preferredHeight;
                _scrollView.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Clamp(height, 0f, _maxHistoryHeight));
                _content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            }
        }


        public void ClearInput()
        {
            if (_input != null)
            {
                _input.text = string.Empty;
            }
        }

        public void SetInput(string input)
        {
            if (_input != null)
            {
                _input.SetTextWithoutNotify(input);
            }
        }

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public string GetActiveInput()
        {
            return _input != null ? _input.text : string.Empty;
        }


        public void ClearHistory()
        {
            if (_history != null)
            {
                _history.text = string.Empty;
            }
        }

        private void OnDestroy()
        {
            if (_resultButton != null)
            {
                _resultButton.onClick.RemoveAllListeners();
            }
        }

    }
}
