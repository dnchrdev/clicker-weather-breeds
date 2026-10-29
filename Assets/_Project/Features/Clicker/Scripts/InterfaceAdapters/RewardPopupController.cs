using System;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Clicker.Scripts.UseCases;
using UniRx;
using Zenject;

namespace _Project.Features.Clicker.Scripts.InterfaceAdapters
{
    public class RewardPopupController: IInitializable, IDisposable
    {
        private readonly OuterRewardUseCase  _outerRewardUseCase;
        private readonly IRewardPopupView _rewardPopupView;

        private CompositeDisposable _disposables = new CompositeDisposable();

        public RewardPopupController(OuterRewardUseCase outerRewardUseCase, IRewardPopupView rewardPopupView)
        {
            _outerRewardUseCase = outerRewardUseCase;
            _rewardPopupView = rewardPopupView;
        }

        public void Initialize()
        {
            _rewardPopupView.RewardArrived
                .Subscribe(reward => _outerRewardUseCase.AddOuterReward(reward))
                .AddTo(_disposables);

            _rewardPopupView.AllSettled
                .Subscribe(_ => _outerRewardUseCase.SyncOuterToInner())
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
