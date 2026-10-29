using System;
using UniRx;

namespace _Project.Features.Clicker.Scripts.Entities
{
    [Serializable]
    public class RewardSettings
    {
        public int WalletStart;
        public int WalletMax;
        public int TapReward;
        public int AutoCollectReward;
    }
    
    public class Wallet
    {
        private int _innerAmount;
        private ReactiveProperty<int> _outerAmount;
        private RewardSettings _config;
        
        public IObservable<int> OuterAmount => _outerAmount;

        public Wallet(RewardSettings config)
        {
            _config = config;
            _innerAmount = config.WalletStart;
            _outerAmount = new ReactiveProperty<int>(config.WalletStart);
            
        }

        public void EqualizeAmount()
        {
            _outerAmount.Value = _innerAmount;
        }
        
        public int TapAddReward()
        {
            _innerAmount = Math.Clamp(_innerAmount + _config.TapReward, 0, _config.WalletMax);
            return _config.TapReward;
        }
        
        public void AddOuterReward(int reward)
        {
            _outerAmount.Value = Math.Clamp(_outerAmount.Value + reward, 0, _innerAmount);
        }

        public int AutoCollectAddReward()
        {
            _innerAmount = Math.Clamp(_innerAmount + _config.AutoCollectReward, 0, _config.WalletMax);
            return _config.AutoCollectReward;
        }
    }
}