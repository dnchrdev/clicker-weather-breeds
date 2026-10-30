using System;
using UniRx;

namespace _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports
{
    public interface IRewardPopupView
    {
        public IObservable<int> RewardArrived { get; }
        public IObservable<Unit> AllArrived { get; }
        
    }
}
