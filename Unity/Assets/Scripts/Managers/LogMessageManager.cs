using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CalorieCounter.Managers
{
    public class LogMessageManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _logMessageCanvas = default;

        [SerializeField]
        private TextMeshProUGUI _messageText = default;

        [SerializeField]
        private TextMeshProUGUI _stackText = default;

        [SerializeField]
        private Button _continueButton = default;

        [SerializeField]
        private Button _quitButton = default;

        private void Awake()
        {
            Application.logMessageReceived += OnLogMessageReceived;
            _quitButton.onClick.AddListener(OnQuitButtonClick);
            _continueButton.onClick.AddListener(OnContinueButtonClick);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClick);
            _quitButton.onClick.RemoveListener(OnQuitButtonClick);
        }

        private void OnLogMessageReceived(string message, string stackTrace, LogType type)
        {
            if (type == LogType.Log)
                return;

            _logMessageCanvas.SetActive(true);
            _messageText.text = message;
            _stackText.text = stackTrace;
        }

        private void OnContinueButtonClick()
        {
            _logMessageCanvas.SetActive(false);
        }

        private void OnQuitButtonClick()
        {
            GUIUtility.systemCopyBuffer = _messageText.text + Environment.NewLine + Environment.NewLine + _stackText.text;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}