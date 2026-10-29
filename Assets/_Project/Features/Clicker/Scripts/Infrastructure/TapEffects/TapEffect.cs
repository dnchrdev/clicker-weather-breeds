using System;
using UniRx;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Infrastructure.TapEffects
{
    public class TapEffect : MonoBehaviour, ITapEffect
    {
        private readonly Subject<Unit> _finished = new Subject<Unit>();

        private ParticleSystem _particles;

        public IObservable<Unit> Finished => _finished;

        public void Play(Transform parent, Vector3 position)
        {
            if (_particles == null)
                _particles = GetComponent<ParticleSystem>();

            transform.SetParent(parent);
            transform.position = position;

            _particles.Clear();
            _particles.Play();
        }

        private void OnParticleSystemStopped()
        {
            _finished.OnNext(Unit.Default);
        }

        public void Hide(Transform parent)
        {
            transform.SetParent(parent);
        }

        void IDisposable.Dispose()
        {
            _finished.OnCompleted();

            if (this != null)
                Destroy(gameObject);
        }
    }
}
