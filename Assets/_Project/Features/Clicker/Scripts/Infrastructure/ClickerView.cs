using System;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Clicker.Scripts.UseCases.Ports;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.Clicker.Scripts.Infrastructure
{
    public class ClickerView: MonoBehaviour, IClickerView, ITapButtonPunch
    {
        [SerializeField] private Button _tapButton;
        [SerializeField] private TMP_Text _wallet;
        [SerializeField] private TMP_Text _energy;
        [SerializeField] private Image _energyFill;
        [SerializeField] private float _punchScale = 0.15f;
        [SerializeField] private float _punchDuration = 0.2f;
        
        private Subject<Unit> _tapClicked = new Subject<Unit>();
        private CompositeDisposable _disposables = new CompositeDisposable();
        private Tween _punchTween;
        private Vector3 _tapButtonBaseScale;
        
        public IObservable<Unit> TapClicked => _tapClicked;

        private void Awake()
        {
            _tapButtonBaseScale = _tapButton.transform.localScale;
        }

        public void Punch()
        {
            _punchTween?.Kill();
            _tapButton.transform.localScale = _tapButtonBaseScale;

            _punchTween = _tapButton.transform
                .DOPunchScale(_tapButtonBaseScale * _punchScale, _punchDuration, 1, 0f)
                .SetLink(_tapButton.gameObject);
        }

        public void UpdateWallet(int amount)
        {
            _wallet.SetText("{0}", amount);
        }

        public void UpdateEnergy(int amount)
        {
            _energy.SetText("{0}", amount);
        }
        
        public void UpdateEnergyFill(float ratio)
        {
            _energyFill.fillAmount = ratio;
        }

        public void ShowView()
        {
            gameObject.SetActive(true);
        }

        public void HideView()
        {
            gameObject.SetActive(false);
        }

        public void SubscribeToButtons()
        {
            _tapButton.OnClickAsObservable()
                .Subscribe(_ => _tapClicked.OnNext(Unit.Default))
                .AddTo(_disposables);
        }

        public void UnsubscribeToButtons()
        {
            _disposables.Clear();
        }

        private void OnDestroy()
        {
            _tapClicked.OnCompleted();
            _disposables.Dispose();
        }
    }
}