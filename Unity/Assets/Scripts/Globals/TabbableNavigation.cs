using CalorieCounter.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CalorieCounter
{

    [RequireComponent(typeof(Selectable))]
    public class TabbableNavigation : MonoBehaviour
    {

        private Selectable _selectable;

        private void Start()
        {
            _selectable = GetComponent<Selectable>();
            GameManager.InputKeyManager.InputKeyPressed += InputKeyManager_OnInputKeyPressed;
        }

        private void OnDestroy()
        {
            GameManager.InputKeyManager.InputKeyPressed -= InputKeyManager_OnInputKeyPressed;
        }

        private void InputKeyManager_OnInputKeyPressed(object sender, InputKeyManager.InputKeyPressedEventArgs e)
        {

            if (e.InputKeyCode == InputKeyCode.SelectNext && EventSystem.current.currentSelectedGameObject == gameObject)
            {
                // Must wait a frame or an endless loop of selecting the selectable on right will occur
                StartCoroutine(SelectAfterFrame(_selectable.FindSelectableOnRight()));
            }
        }

        private IEnumerator SelectAfterFrame(Selectable selectable)
        {
            yield return new WaitForEndOfFrame();
            selectable.Select();
        }
    }
}