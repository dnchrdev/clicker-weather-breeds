using _Project.Features.Clicker.Scripts.Entities;

namespace _Project.Features.Clicker.Scripts.UseCases
{
    public class OuterRewardUseCase
    {
        private readonly Wallet _wallet;

        public OuterRewardUseCase(Wallet wallet)
        {
            _wallet = wallet;
        }

        public void SyncOuterToInner()
        {
            _wallet.EqualizeAmount();
        }
        
        public void AddOuterReward(int reward)
        {
            _wallet.AddOuterReward(reward);
        }
    }
}