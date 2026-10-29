using _Project.Features.Clicker.Scripts.Entities;
using UniRx;

namespace _Project.Features.Clicker.Scripts.UseCases
{
    public class AutoCollectUseCase
    {
        private readonly IEnergy _energy;
        private readonly Wallet _wallet;
        private readonly IAutoCollectRequest  _autoCollectRequest;
        private readonly RewardPopupUseCase _rewardPopupUseCase;

        public AutoCollectUseCase(IEnergy energy, Wallet wallet, IAutoCollectRequest autoCollectRequest, RewardPopupUseCase rewardPopupUseCase)
        {
            _energy = energy;
            _wallet = wallet;
            _autoCollectRequest = autoCollectRequest;
            _rewardPopupUseCase = rewardPopupUseCase;
        }

        public void Initialize(CompositeDisposable disposables)
        {
            _autoCollectRequest.AutoCollectRequest
                .Subscribe(_ => AutoCollect())
                .AddTo(disposables);
        }

        private void AutoCollect()
        {
            if (_energy.TryAutoCollectConsume())
            {
                int reward = _wallet.AutoCollectAddReward();
                _rewardPopupUseCase.Raise(reward);
            }
        }
    }
}