using UnityEngine;
using TMPro;

namespace CalorieCounter.Menu
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class VersionText : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<TextMeshProUGUI>().text = Application.version;
        }

    }
}
