using UnityEngine;

namespace CalorieCounter
{
    public abstract class AbstractAdapter : MonoBehaviour
    {
        public abstract void Export();
        public abstract bool DoDifferencesExist();
    }
}
