using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace MSCalc.Dialog
{
    public class DialogView : MonoBehaviour, IDialogView
    {
        [SerializeField]
        private GameObject _dialogRoot;
        [SerializeField]
        private TMP_Text _messageText;
        [SerializeField]
        private Button _closeButton;

        public event Action OnClosed;

        private void Awake()
        {
            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(() => OnClosed?.Invoke());
            }
            
            SetVisible(false);
        }

        public void SetMessage(string message)
        {
            if (_messageText != null)
                _messageText.text = message;
        }

        public void SetVisible(bool visible)
        {
            if (_dialogRoot != null)
                _dialogRoot.SetActive(visible);
        }

        private void OnDestroy()
        {
            if (_closeButton != null)
            {
                _closeButton.onClick.RemoveListener(() => OnClosed?.Invoke());
            }
        }
    }
}