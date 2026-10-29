using System;
using _Project.Core.Scripts.Entities;
using _Project.Core.Scripts.UseCases;
using _Project.Features.Clicker.Scripts.Entities;
using UniRx;

namespace _Project.Features.Clicker.Scripts.UseCases
{
    public class ClickerTabState: ITabState
    {
        private readonly IAutoCollectIntervalTimer  _autoCollectTimer;
        private readonly IEnergyRegeneration _energyRegeneration;
        
        private ReactiveProperty<bool> _isActive = new ReactiveProperty<bool>(false);
        
        public IObservable<bool> IsActive => _isActive;
        
        public TabType Type =>  TabType.CLICKER;

        public ClickerTabState(IAutoCollectIntervalTimer autoCollectTimer, IEnergyRegeneration energyRegeneration)
        {
            _autoCollectTimer = autoCollectTimer;
            _energyRegeneration = energyRegeneration;
        }

        public void Enter()
        {
            _isActive.Value = true;
        }

        public void Tick(float dt)
        {
            _autoCollectTimer.Advance(dt);
            _energyRegeneration.Regenerate(dt);
        }

        public void Exit()
        {
            _isActive.Value = false;
        }
    }
}