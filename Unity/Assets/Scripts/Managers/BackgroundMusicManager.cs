using CalorieCounter.Audio;
using System.Collections;
using UnityEngine;

namespace CalorieCounter.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusicManager : MonoBehaviour
    {
        [SerializeField]
        private Playlist[] _playlists = default;

        private const float FadeTime = 10;
        private const float MaxAudioSourceVolume = 0.2f;

        private AudioSource _audioSource;
        private Coroutine _fadeInCoroutine;
        private Coroutine _fadeOutCoroutine;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            var currentPlaylist = _playlists[Random.Range(0, _playlists.Length)];
            StartCoroutine(PlayPlaylist(currentPlaylist));
        }

        private IEnumerator PlayPlaylist(Playlist playlist)
        {
            while (true)
            {
                var nextAudioClip = playlist.audioClips[Random.Range(0, playlist.audioClips.Length)];
                _fadeInCoroutine = StartCoroutine(FadeIn(nextAudioClip));
                yield return _fadeInCoroutine;
                yield return new WaitForSecondsRealtime(nextAudioClip.length - FadeTime);
                _fadeOutCoroutine = StartCoroutine(FadeOut());
                yield return _fadeOutCoroutine;
            }
        }

        private IEnumerator FadeIn(AudioClip audioClip)
        {
            _audioSource.volume = 0;
            _audioSource.clip = audioClip;
            _audioSource.Play();
            while (_audioSource.volume < MaxAudioSourceVolume)
            {
                _audioSource.volume += Time.deltaTime / FadeTime * MaxAudioSourceVolume;
                yield return null;
            }
            _audioSource.volume = MaxAudioSourceVolume;
        }

        private IEnumerator FadeOut()
        {
            while (_audioSource.volume > 0)
            {
                _audioSource.volume -= Time.deltaTime / FadeTime * MaxAudioSourceVolume;
                yield return null;
            }
            _audioSource.volume = 0;
            _audioSource.Stop();
        }
    }
}
