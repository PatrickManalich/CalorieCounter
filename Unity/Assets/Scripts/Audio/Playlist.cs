using UnityEngine;

namespace CalorieCounter.Audio
{
    [CreateAssetMenu(fileName = "DayMealPattern", menuName = "Calorie Counter/Playlist")]
    public class Playlist : ScriptableObject
    {
        public AudioClip[] audioClips;
    }
}
