using System;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Clicker.Scripts.UseCases;
using UniRx;
using Zenject;

namespace _Project.Features.Clicker.Scripts.InterfaceAdapters
{
    public class ClickerController : IInitializable, IDisposable
    {
        private readonly TapUseCase _tapUseCase;
        private readonly AutoCollectUseCase _autoCollectUseCase;
        private readonly IClickerView _clickerView;

        private CompositeDisposable _disposables = new CompositeDisposable();

        public ClickerController(TapUseCase tapUseCase, AutoCollectUseCase autoCollectUseCase, IClickerView clickerView)
        {
            _tapUseCase = tapUseCase;
            _autoCollectUseCase = autoCollectUseCase;
            _clickerView = clickerView;
        }

        public void Initialize()
        {
            _clickerView.TapClicked
                .Subscribe(_ => _tapUseCase.Tap())
                .AddTo(_disposables);

            _autoCollectUseCase.Initialize(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}