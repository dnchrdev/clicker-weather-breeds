using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Clicker.Scripts.UseCases.Ports;

namespace _Project.Features.Clicker.Scripts.InterfaceAdapters
{
    public class RewardPopupPresenter : IRaiseRewardPopup
    {
        private readonly IRewardPopupView _rewardPopupView;

        public RewardPopupPresenter(IRewardPopupView rewardPopupView)
        {
            _rewardPopupView = rewardPopupView;
        }

        public void Raise(int reward)
        {
            SpawnRewardPopup(reward);
        }

        private void SpawnRewardPopup(int reward)
        {
            _rewardPopupView.SpawnRewardPopup(reward);
        }
    }
}