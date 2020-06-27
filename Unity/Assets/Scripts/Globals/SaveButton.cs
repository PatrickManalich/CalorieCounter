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
        private List<ScrollViewAssistant> _scrollViewAssistants = default;

        private Button _button;

        protected virtual void Start()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(Button_OnClick);
            foreach(var scrollViewAssistant in _scrollViewAssistants)
            {
                scrollViewAssistant.RowAdded += ScrollView_OnRowAdded;
                scrollViewAssistant.RowRemoved += ScrollView_OnRowRemoved;
            }
            _button.interactable = false;
        }

        protected virtual void OnDestroy()
        {
            foreach (var scrollViewAssistant in _scrollViewAssistants)
            {
                scrollViewAssistant.RowRemoved -= ScrollView_OnRowRemoved;
                scrollViewAssistant.RowAdded -= ScrollView_OnRowAdded;
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

        private void ScrollView_OnRowRemoved(object sender, ScrollViewAssistant.RowChangedEventArgs e)
        {
            RefreshButtonInteractability();
        }

        private void ScrollView_OnRowAdded(object sender, ScrollViewAssistant.RowChangedEventArgs e)
        {
            RefreshButtonInteractability();
        }
    }
}
