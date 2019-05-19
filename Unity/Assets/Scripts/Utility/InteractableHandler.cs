using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter {

    public class InteractableHandler : MonoBehaviour {

        [System.Serializable]
        private class SerializableSourceDictionary : SerializableDictionaryBase<GameObject, SerializableSource> { }

        [System.Serializable]
        private class SerializableSource {
            public bool SetSourceActive = default;
            public List<SerializableTarget> Targets = default;
        }

        [System.Serializable]
        private class SerializableTarget {
            public GameObject Target = default;
            public bool SetActive = default;
        }

        [SerializeField]
        private SerializableSourceDictionary _serializedSourceDictionary = default;

        private void Awake() {
            foreach(var source in _serializedSourceDictionary.Keys) {
                if (source.GetComponent<Button>())
                    source.GetComponent<Button>().onClick.AddListener(delegate { OnSourceInvoked(source); });
            }
        }

        private void OnSourceInvoked(GameObject source) {
            if (!_serializedSourceDictionary.ContainsKey(source))
                return;

            var serializedSource = _serializedSourceDictionary[source];
            source.SetActive(serializedSource.SetSourceActive);
            foreach (var serializedTarget in serializedSource.Targets) {
                serializedTarget.Target.SetActive(serializedTarget.SetActive);
            }
        }
    }
}
