using _Project.Features.Clicker.Scripts.Entities;

namespace _Project.Features.Clicker.Scripts.UseCases
{
    public class TapUseCase
    {
        private readonly IEnergy _energy;
        private readonly Wallet _wallet;
        private readonly RewardPopupUseCase _rewardPopupUseCase;

        public TapUseCase(IEnergy energy, Wallet wallet, RewardPopupUseCase rewardPopupUseCase)
        {
            _energy = energy;
            _wallet = wallet;
            _rewardPopupUseCase = rewardPopupUseCase;
        }

        public void Tap()
        {
            if (_energy.TryTapConsume())
            {
                int reward = _wallet.TapAddReward();
                _rewardPopupUseCase.Raise(reward);
            }
                
        }
    }
}