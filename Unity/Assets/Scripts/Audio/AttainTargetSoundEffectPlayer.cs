using CalorieCounter.Managers;
using CalorieCounter.MealEntries;
using UnityEngine;

namespace CalorieCounter.Audio
{
    [RequireComponent(typeof(Totals))]
    public class AttainTargetSoundEffectPlayer : MonoBehaviour
    {
        private Totals _totals;
        private bool _targetAttained;

        private void Start()
        {
            _totals = GetComponent<Totals>();

            _totals.TargetAttained += Totals_OnTargetAttained;
        }

        private void OnDestroy()
        {
            _totals.TargetAttained -= Totals_OnTargetAttained;
        }

        private void Update()
        {
            if(_targetAttained)
            {
                GameManager.SoundEffectsManager.PlayAttainTargetSoundEffect();
                _targetAttained = false;
            }
        }

        private void Totals_OnTargetAttained(object sender, System.EventArgs e)
        {
            _targetAttained = true;
        }
    }
}
