using System;
using UniRx;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Entities
{

    public interface IAutoCollectIntervalTimer
    {
        public void Advance(float dt);
    }
    
    public interface IAutoCollectRequest
    {
        public IObservable<Unit> AutoCollectRequest { get; }
    }
    
    [Serializable]
    public class AutoCollectSettings
    {
        public float IntervalSeconds;
    }
    
    public class AutoCollectAutoCollectIntervalTimer: IAutoCollectIntervalTimer, IAutoCollectRequest
    {
        private float _timer;
        private AutoCollectSettings  _config;

        private readonly Subject<Unit> _autoCollectRequest = new Subject<Unit>();

        public IObservable<Unit> AutoCollectRequest => _autoCollectRequest;
        
        public AutoCollectAutoCollectIntervalTimer(AutoCollectSettings  config)
        {
            _config = config;
            _timer = 0f;
        }
        
        public void Advance(float dt)
        {
            _timer = Mathf.Max(_timer + dt, 0f);

            if (_timer >= _config.IntervalSeconds)
            {
                _autoCollectRequest.OnNext(Unit.Default);
                _timer = Mathf.Max(_timer - _config.IntervalSeconds, 0f);
            }
        }
        
    }
}