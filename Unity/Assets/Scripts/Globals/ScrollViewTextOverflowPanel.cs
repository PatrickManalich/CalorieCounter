using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ScrollViewTextOverflowPanel : MonoBehaviour
    {

        [SerializeField]
        private ScrollViewRowHighlighter _scrollViewRowHighlighter = default;

        [SerializeField]
        private Image _arrowImage = default;

        [SerializeField]
        private TextMeshProUGUI _overflowText = default;

        private const float DelayFadeSeconds = 2;
        private const float FadeInSeconds = 0.5f;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Coroutine _fadeInCoroutine;
        private float _fadeInTimer;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _scrollViewRowHighlighter.EnteredHighlightRow += ScrollViewRowHighlighter_OnEnteredHighlightRow;
            _scrollViewRowHighlighter.ExitedHighlightRow += ScrollViewRowHighlighter_OnExitedHighlightRow;

            _canvasGroup.alpha = 0;
        }

        private void OnDestroy()
        {
            StopFadeInCoroutine();
            _scrollViewRowHighlighter.ExitedHighlightRow -= ScrollViewRowHighlighter_OnExitedHighlightRow;
            _scrollViewRowHighlighter.EnteredHighlightRow -= ScrollViewRowHighlighter_OnEnteredHighlightRow;
        }

        private void ScrollViewRowHighlighter_OnEnteredHighlightRow(object sender, System.EventArgs e)
        {
            StopFadeInCoroutine();
            if (_scrollViewRowHighlighter.HighlightedScrollViewText.Text.isTextTruncated)
            {
                _fadeInCoroutine = StartCoroutine(FadeIn());
            }
        }

        private void ScrollViewRowHighlighter_OnExitedHighlightRow(object sender, System.EventArgs e)
        {
            StopFadeInCoroutine();
            _canvasGroup.alpha = 0;
        }

        private void StopFadeInCoroutine()
        {
            if (_fadeInCoroutine != null)
            {
                StopCoroutine(_fadeInCoroutine);
                _fadeInCoroutine = null;
            }
        }

        private IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(DelayFadeSeconds);

            if(_scrollViewRowHighlighter.HighlightedRowIndex == 0)
            {
                _rectTransform.anchorMin = new Vector2(0, 1);
                _rectTransform.anchorMax = new Vector2(1, 1);
                _rectTransform.pivot = new Vector2(0.5f, 1);
                _rectTransform.anchoredPosition = new Vector2(0, -Mathf.Abs(_rectTransform.anchoredPosition.y));

                _arrowImage.rectTransform.anchorMin = new Vector2(0.5f, 1);
                _arrowImage.rectTransform.anchorMax = new Vector2(0.5f, 1);
                _arrowImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                _arrowImage.rectTransform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                _rectTransform.anchorMin = new Vector2(0, 0);
                _rectTransform.anchorMax = new Vector2(1, 0);
                _rectTransform.pivot = new Vector2(0.5f, 0);
                _rectTransform.anchoredPosition = new Vector2(0, Mathf.Abs(_rectTransform.anchoredPosition.y));

                _arrowImage.rectTransform.anchorMin = new Vector2(0.5f, 0);
                _arrowImage.rectTransform.anchorMax = new Vector2(0.5f, 0);
                _arrowImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                _arrowImage.rectTransform.rotation = Quaternion.Euler(0, 0, 270);
            }

            var highlightedScrollViewText = _scrollViewRowHighlighter.HighlightedScrollViewText.Text;
            _arrowImage.rectTransform.position = new Vector2(highlightedScrollViewText.rectTransform.position.x, _arrowImage.rectTransform.position.y);
            _overflowText.text = highlightedScrollViewText.text;
            _fadeInTimer = 0;
            while(_fadeInTimer < FadeInSeconds)
            {
                yield return null;
                _canvasGroup.alpha = _fadeInTimer / FadeInSeconds;
                _fadeInTimer += Time.deltaTime;
            }
            _canvasGroup.alpha = 1;
            _fadeInCoroutine = null;
        }
    }
}