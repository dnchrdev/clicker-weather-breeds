using _Project.Features.Clicker.Scripts.UseCases.Ports;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Infrastructure
{
    public class TapSfx : MonoBehaviour, ITapSfx
    {
        private const int SourcesCount = 4;

        [SerializeField] private AudioClip _clip;
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;
        [SerializeField] private float _pitchMin = 0.95f;
        [SerializeField] private float _pitchMax = 1.05f;

        private AudioSource[] _sources;
        private int _nextSource;

        public void Play()
        {
            if (_clip == null)
                return;

            if (_sources == null)
                CreateSources();

            var source = _sources[_nextSource];
            _nextSource = (_nextSource + 1) % _sources.Length;

            source.pitch = Random.Range(_pitchMin, _pitchMax);
            source.PlayOneShot(_clip, _volume);
        }

        private void CreateSources()
        {
            _sources = new AudioSource[SourcesCount];

            for (int i = 0; i < SourcesCount; i++)
            {
                var source = gameObject.AddComponent<AudioSource>();

                source.playOnAwake = false;
                source.spatialBlend = 0f;

                _sources[i] = source;
            }
        }
    }
}