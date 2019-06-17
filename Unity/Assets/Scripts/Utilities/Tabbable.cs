﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CalorieCounter.Utilities {

    public class Tabbable : MonoBehaviour, IUpdateSelectedHandler {

        public Selectable NextSelectable;

        public void OnUpdateSelected(BaseEventData data) {
            if (Input.GetKeyDown(KeyCode.Tab)) {
                NextSelectable.Select();
            }
        }
    }
}
