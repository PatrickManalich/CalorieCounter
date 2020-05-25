using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{

    [RequireComponent(typeof(Button))]
    public class ExportButton : MonoBehaviour
    {
        [SerializeField]
        private List<AbstractAdapter> _adapters = default;

        [SerializeField]
        private List<ScrollView> _scrollViews = default;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            foreach(var adapter in _adapters)
            {
                _button.onClick.AddListener(adapter.Export);
            }
            foreach(var scrollView in _scrollViews)
            {
                scrollView.RowChanged += RefreshButtonInteractability;
            }
            _button.interactable = false;
        }

        private void OnDestroy()
        {
            foreach (var scrollView in _scrollViews)
            {
                scrollView.RowChanged -= RefreshButtonInteractability;
            }
            foreach (var adapter in _adapters)
            {
                _button.onClick.RemoveListener(adapter.Export);
            }
        }

        private void RefreshButtonInteractability()
        {
            var doDifferencesExist = false;
            foreach (var adapter in _adapters)
            {
                if (adapter.DoDifferencesExist())
                {
                    doDifferencesExist = true;
                    break;
                }
            }
            _button.interactable = doDifferencesExist;
        }
    }
}
