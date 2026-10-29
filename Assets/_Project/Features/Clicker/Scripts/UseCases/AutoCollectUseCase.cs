using _Project.Features.Clicker.Scripts.Entities;
using _Project.Features.Clicker.Scripts.UseCases.Ports;
using UniRx;

namespace _Project.Features.Clicker.Scripts.UseCases
{
    public class AutoCollectUseCase
    {
        private readonly IEnergy _energy;
        private readonly Wallet _wallet;
        private readonly IAutoCollectRequest  _autoCollectRequest;
        private readonly IRaiseRewardPopup _raiseRewardPopup;
        private readonly ITapSfx _tapSfx;
        private readonly ITapButtonPunch _tapButtonPunch;

        public AutoCollectUseCase(IEnergy energy, Wallet wallet, IAutoCollectRequest autoCollectRequest, IRaiseRewardPopup raiseRewardPopup, ITapSfx tapSfx, ITapButtonPunch tapButtonPunch)
        {
            _energy = energy;
            _wallet = wallet;
            _autoCollectRequest = autoCollectRequest;
            _raiseRewardPopup = raiseRewardPopup;
            _tapSfx = tapSfx;
            _tapButtonPunch = tapButtonPunch;
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
                _tapSfx.Play();
                _tapButtonPunch.Punch();

                int reward = _wallet.AddAutoCollectReward();
                _raiseRewardPopup.Raise(reward);
            }
        }
    }
}