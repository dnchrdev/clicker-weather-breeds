using System;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Clicker.Scripts.UseCases;
using UniRx;
using Zenject;

namespace _Project.Features.Clicker.Scripts.InterfaceAdapters
{
    public class RewardPopupPresenter: IInitializable, IDisposable
    {
        private readonly RewardPopupUseCase _rewardPopupUseCase;
        private readonly IRewardPopupView  _rewardPopupView;
        
        private CompositeDisposable _disposable = new CompositeDisposable();

        public RewardPopupPresenter(RewardPopupUseCase rewardPopupUseCase, IRewardPopupView rewardPopupView)
        {
            _rewardPopupUseCase = rewardPopupUseCase;
            _rewardPopupView = rewardPopupView;
        }
        
        public void Initialize()
        {
            _rewardPopupUseCase.RewardPopup
                .Subscribe(reward => SpawnRewardPopup(reward))
                .AddTo(_disposable);
        }

        private void SpawnRewardPopup(int reward)
        {
            _rewardPopupView.SpawnRewardPopup(reward);
        }

        public void Dispose()
        {
         _disposable.Dispose();   
        }
    }
}
