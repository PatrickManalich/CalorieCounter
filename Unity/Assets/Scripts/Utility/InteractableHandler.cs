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
            public bool InteractableAfterAwake = default;
            public bool InteractableAfterExecute = default;
            public List<SerializableTarget> Targets = default;
        }

        [System.Serializable]
        private class SerializableTarget {
            public GameObject Target = default;
            public bool InteractableAfterExecute = default;
        }

        [SerializeField]
        private SerializableSourceDictionary _serializedSourceDictionary = default;

        public void Execute(GameObject source) {
            if (!_serializedSourceDictionary.ContainsKey(source))
                return;

            var serializedSource = _serializedSourceDictionary[source];
            source.GetComponent<Selectable>().interactable = serializedSource.InteractableAfterExecute;
            foreach (var serializedTarget in serializedSource.Targets) {
                serializedTarget.Target.GetComponent<Selectable>().interactable = serializedTarget.InteractableAfterExecute;
            }
        }

        public void UndoExecute(GameObject source) {
            if (!_serializedSourceDictionary.ContainsKey(source))
                return;

            foreach (var serializedTarget in _serializedSourceDictionary[source].Targets) {
                serializedTarget.Target.GetComponent<Selectable>().interactable = !serializedTarget.InteractableAfterExecute;
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
                    source.GetComponent<Button>().onClick.AddListener(delegate { Execute(source); });
                source.GetComponent<Selectable>().interactable = _serializedSourceDictionary[source].InteractableAfterAwake;
            }
        }
    }
}
