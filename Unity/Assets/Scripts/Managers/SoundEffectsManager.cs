using UnityEngine;

namespace CalorieCounter.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectsManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _successfulClickAudioClip = default;

        [SerializeField]
        private AudioClip _failedClickAudioClip = default;

        [SerializeField]
        private AudioClip _scrollAudioClip = default;

        [SerializeField]
        private AudioClip _typeAudioClip = default;

        [SerializeField]
        private AudioClip _pressInputKeyAudioClip = default;

        [SerializeField]
        private AudioClip _attainTargetAudioClip = default;

        private AudioSource _audioSource;

        public void PlaySuccessfulClickSoundEffect()
        {
            PlaySoundEffect(_successfulClickAudioClip);
        }

        public void PlayFailedClickSoundEffect()
        {
            PlaySoundEffect(_failedClickAudioClip);
        }

        public void PlayScrollSoundEffect()
        {
            PlaySoundEffect(_scrollAudioClip);
        }

        public void PlayTypeSoundEffect()
        {
            PlaySoundEffect(_typeAudioClip);
        }

        public void PlayPressInputKeySoundEffect()
        {
            PlaySoundEffect(_pressInputKeyAudioClip);
        }

        public void PlayAttainTargetSoundEffect()
        {
            PlaySoundEffect(_attainTargetAudioClip);
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void PlaySoundEffect(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}
