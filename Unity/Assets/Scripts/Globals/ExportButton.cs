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

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            foreach(var adapter in _adapters)
            {
                _button.onClick.AddListener(() => adapter.Export());
            }
        }

        private void OnDestroy()
        {
            foreach (var adapter in _adapters)
            {
                _button.onClick.RemoveListener(() => adapter.Export());
            }
        }
    }

}
