using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{

    [RequireComponent(typeof(Button))]
    public class SaveButton : MonoBehaviour
    {
        [SerializeField]
        private List<AbstractAdapter> _adapters = default;

        [SerializeField]
        private List<ScrollView> _scrollViews = default;

        private Button _button;

        protected virtual void Start()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(Button_OnClick);
            foreach(var scrollView in _scrollViews)
            {
                scrollView.RowAdded += ScrollView_OnRowAdded;
                scrollView.RowRemoved += ScrollView_OnRowRemoved;
            }
            _button.interactable = false;
        }

        protected virtual void OnDestroy()
        {
            foreach (var scrollView in _scrollViews)
            {
                scrollView.RowRemoved -= ScrollView_OnRowRemoved;
                scrollView.RowAdded -= ScrollView_OnRowAdded;
            }
            _button.onClick.RemoveListener(Button_OnClick);
        }

        protected void RefreshButtonInteractability()
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

        private void Button_OnClick()
        {
            foreach (var adapter in _adapters)
            {
                adapter.Export();
            }
            _button.interactable = false;
        }

        private void ScrollView_OnRowRemoved(object sender, ScrollView.RowChangedEventArgs e)
        {
            RefreshButtonInteractability();
        }

        private void ScrollView_OnRowAdded(object sender, ScrollView.RowChangedEventArgs e)
        {
            RefreshButtonInteractability();
        }
    }
}
