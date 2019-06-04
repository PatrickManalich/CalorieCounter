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
            public bool StartInteractable = default;
            public bool InteractableAfterInvoked = default;
            public List<SerializableTarget> Targets = default;
        }

        [System.Serializable]
        private class SerializableTarget {
            public GameObject Target = default;
            public bool InteractableAfterInvoked = default;
        }

        [SerializeField]
        private SerializableSourceDictionary _serializedSourceDictionary = default;

        public void SetSourceAndTargetsInteractable(GameObject source) {
            if (!_serializedSourceDictionary.ContainsKey(source))
                return;

            var serializedSource = _serializedSourceDictionary[source];
            source.GetComponent<Selectable>().interactable = serializedSource.InteractableAfterInvoked;
            foreach (var serializedTarget in serializedSource.Targets) {
                serializedTarget.Target.GetComponent<Selectable>().interactable = serializedTarget.InteractableAfterInvoked;
            }
        }

        private void Awake() {
            foreach(var source in _serializedSourceDictionary.Keys) {
                if (source.GetComponent<Selectable>() == null) {
                    Debug.LogError("All sources must have the Selectable component attached.");
                    return;
                }
                var serializedSource = _serializedSourceDictionary[source];
                foreach (var serializedTarget in serializedSource.Targets) {
                    if (serializedTarget.Target.GetComponent<Selectable>() == null) {
                        Debug.LogError("All targets must have the Selectable component attached.");
                        return;
                    }
                }

                if (source.GetComponent<Button>())
                    source.GetComponent<Button>().onClick.AddListener(delegate { SetSourceAndTargetsInteractable(source); });
                source.GetComponent<Selectable>().interactable = _serializedSourceDictionary[source].StartInteractable;
            }
        }
    }
}
