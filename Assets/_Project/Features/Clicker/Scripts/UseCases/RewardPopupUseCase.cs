using System;
using UniRx;

namespace _Project.Features.Clicker.Scripts.UseCases
{
    public class RewardPopupUseCase
    {
        private readonly Subject<int> _rewardPopup= new Subject<int>();
        public IObservable<int> RewardPopup => _rewardPopup;
        
        public void Raise(int reward)
        {
            _rewardPopup.OnNext(reward);
        }
    }
}