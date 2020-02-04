using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.Utilities {

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
            if (source.GetComponent<Selectable>() != null)
                source.GetComponent<Selectable>().interactable = serializedSource.InteractableAfterExecute;

            foreach (var serializedTarget in serializedSource.Targets) {
                if (serializedTarget.Target.GetComponent<Selectable>() != null)
                    serializedTarget.Target.GetComponent<Selectable>().interactable = serializedTarget.InteractableAfterExecute;
            }
        }

        public void UndoExecute(GameObject source) {
            if (!_serializedSourceDictionary.ContainsKey(source))
                return;

            foreach (var serializedTarget in _serializedSourceDictionary[source].Targets) {
                if (serializedTarget.Target.GetComponent<Selectable>() != null)
                    serializedTarget.Target.GetComponent<Selectable>().interactable = !serializedTarget.InteractableAfterExecute;
            }
        }

        private void Awake() {
            foreach(var source in _serializedSourceDictionary.Keys) {
                if (source.GetComponent<Button>())
                    source.GetComponent<Button>().onClick.AddListener(() => Execute(source));

                if (source.GetComponent<Selectable>() != null)
                    source.GetComponent<Selectable>().interactable = _serializedSourceDictionary[source].InteractableAfterAwake;
            }
        }

        private void OnDestroy()
        {
            foreach (var source in _serializedSourceDictionary.Keys)
            {
                if (source != null && source.GetComponent<Button>())
                    source.GetComponent<Button>().onClick.RemoveListener(() => Execute(source));
            }
        }
    }
}
