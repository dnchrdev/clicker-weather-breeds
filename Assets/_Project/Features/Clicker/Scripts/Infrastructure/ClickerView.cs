using System;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.Clicker.Scripts.Infrastructure
{
    public class ClickerView: MonoBehaviour, IClickerView
    {
        [SerializeField] private Button _tapButton;
        
        private Subject<Unit> _tapClicked = new Subject<Unit>();
        private CompositeDisposable _disposables = new CompositeDisposable();
        
        public IObservable<Unit> TapClicked => _tapClicked;
        
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