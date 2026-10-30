using _Project.Features.Clicker.Scripts.Entities;
using _Project.Features.Clicker.Scripts.UseCases.Ports;

namespace _Project.Features.Clicker.Scripts.UseCases
{
    public class TapUseCase
    {
        private readonly IEnergy _energy;
        private readonly Wallet _wallet;
        private readonly IRaiseRewardPopup _raiseRewardPopup;
        private readonly ITapSfx _tapSfx;
        private readonly ITapButtonPunch _tapButtonPunch;

        public TapUseCase(IEnergy energy, Wallet wallet, IRaiseRewardPopup raiseRewardPopup, ITapSfx tapSfx, ITapButtonPunch tapButtonPunch)
        {
            _energy = energy;
            _wallet = wallet;
            _raiseRewardPopup =  raiseRewardPopup;
            _tapSfx = tapSfx;
            _tapButtonPunch = tapButtonPunch;
        }

        public void Tap()
        {
            if (_energy.TryTapConsume())
            {
                _tapSfx.Play();
                _tapButtonPunch.Punch();
                int reward = _wallet.AddTapReward();
                _raiseRewardPopup.ShowRewardPopup(reward);
            }
                
        }
    }
}
