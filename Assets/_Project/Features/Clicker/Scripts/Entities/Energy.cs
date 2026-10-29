using System;
using UniRx;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Entities
{
    public interface IEnergyRegeneration
    {
        public void Regenerate(float dt);
    }

    public interface IEnergy
    {
        public IObservable<int> Amount { get; }
        public IObservable<float> Ratio { get; }

        public bool TryTapConsume();
        public bool TryAutoCollectConsume();
    }
    
    [Serializable]
    public class EnergySettings
    {
        public int Max;
        public int Start;
        public int TapCost;
        public int AutoCollectCost;
        public int RegenAmount;
        public float RegenIntervalSeconds;
    }
    
    public class Energy: IEnergy, IEnergyRegeneration
    {
        private ReactiveProperty<int> _amount;
        private ReactiveProperty<float> _ratio;
        private EnergySettings _config;

        private float _regenerationTimer;
        
        public IObservable<int> Amount => _amount;
        public IObservable<float> Ratio => _ratio;

        public Energy(EnergySettings config)
        {
            _config = config;
            _amount = new ReactiveProperty<int>(config.Start);
            _ratio = new ReactiveProperty<float>(config.Start * 1f/_config.Max);
            _regenerationTimer = 0f;
        }

        public bool TryTapConsume()
        {
            if (_amount.Value >= _config.TapCost)
            {
                _amount.Value = Math.Clamp(_amount.Value - _config.TapCost, 0, _config.Max);
                UpdateRatio();
                return true;
            }

            return false;
        }

        public bool TryAutoCollectConsume()
        {
            if (_amount.Value >= _config.AutoCollectCost)
            {
                _amount.Value = Math.Clamp(_amount.Value - _config.AutoCollectCost, 0, _config.Max);
                UpdateRatio();
                return true;
            }

            return false;
        }

        public void Regenerate(float dt)
        {
            _regenerationTimer = Math.Max(_regenerationTimer + dt, 0);

            if (_regenerationTimer >= _config.RegenIntervalSeconds)
            {
                _regenerationTimer = Mathf.Max(_regenerationTimer - _config.RegenIntervalSeconds, 0f);
                _amount.Value = Math.Clamp(_amount.Value + _config.RegenAmount, 0, _config.Max);
                UpdateRatio();
            }
        }
        
        private void UpdateRatio()
        {
            _ratio.Value = Math.Clamp(_amount.Value * 1f / _config.Max, 0f, 1f);
        }
        
    }
}