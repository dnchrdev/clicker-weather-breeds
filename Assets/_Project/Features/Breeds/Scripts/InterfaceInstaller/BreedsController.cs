using System;
using _Project.Features.Breeds.Scripts.InterfaceInstaller.Ports;
using _Project.Features.Breeds.Scripts.UseCases;
using UniRx;
using Zenject;

namespace _Project.Features.Breeds.Scripts.InterfaceInstaller
{
    public class BreedsController : IInitializable, IDisposable
    {
        private readonly IBreedsView _breedsView;
        private readonly LoadBreedDetailsUseCase _loadBreedDetailsUseCase;
        private readonly CloseBreedDetailsPopupUseCase _closeBreedDetailsPopupUseCase;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public BreedsController(
            IBreedsView breedsView,
            LoadBreedDetailsUseCase loadBreedDetailsUseCase,
            CloseBreedDetailsPopupUseCase closeBreedDetailsPopupUseCase)
        {
            _breedsView = breedsView;
            _loadBreedDetailsUseCase = loadBreedDetailsUseCase;
            _closeBreedDetailsPopupUseCase = closeBreedDetailsPopupUseCase;
        }

        public void Initialize()
        {
            _breedsView.CardClicked
                .Subscribe(breedId => _loadBreedDetailsUseCase.Load(breedId))
                .AddTo(_disposables);

            _breedsView.PopupCloseClicked
                .Subscribe(_ => _closeBreedDetailsPopupUseCase.Close())
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
