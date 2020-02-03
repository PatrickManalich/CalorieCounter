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
            Application.logMessageReceived += Application_OnLogMessageReceived;
            _quitButton.onClick.AddListener(QuitButton_OnClick);
            _continueButton.onClick.AddListener(ContinueButton_OnClick);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(ContinueButton_OnClick);
            _quitButton.onClick.RemoveListener(QuitButton_OnClick);
        }

        private void Application_OnLogMessageReceived(string message, string stackTrace, LogType type)
        {
            if (type == LogType.Log)
                return;

            _logMessageCanvas.SetActive(true);
            _messageText.text = message;
            _stackText.text = stackTrace;
        }

        private void ContinueButton_OnClick()
        {
            _logMessageCanvas.SetActive(false);
        }

        private void QuitButton_OnClick()
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