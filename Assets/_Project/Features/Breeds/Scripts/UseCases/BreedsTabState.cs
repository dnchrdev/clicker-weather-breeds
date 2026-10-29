using System;
using _Project.Core.Scripts.Entities;
using _Project.Core.Scripts.UseCases;
using _Project.Features.Breeds.Scripts.Entities;
using UniRx;

namespace _Project.Features.Breeds.Scripts.UseCases
{
    public class BreedsTabState: ITabState
    {
        private readonly LoadBreedsUseCase _loadBreedsUseCase;
        private readonly LoadBreedDetailsUseCase _loadBreedDetailsUseCase;
        private readonly BreedPopupInfo _breedPopupInfo;

        private ReactiveProperty<bool> _isActive = new ReactiveProperty<bool>(false);
        
        public IObservable<bool> IsActive => _isActive;
        
        public TabType Type =>  TabType.BREEDS;

        public BreedsTabState(
            LoadBreedsUseCase loadBreedsUseCase,
            LoadBreedDetailsUseCase loadBreedDetailsUseCase,
            BreedPopupInfo breedPopupInfo)
        {
            _loadBreedsUseCase = loadBreedsUseCase;
            _loadBreedDetailsUseCase = loadBreedDetailsUseCase;
            _breedPopupInfo = breedPopupInfo;
        }

        public void Enter()
        {
            _isActive.Value = true;
            _loadBreedsUseCase.Load();
        }

        public void Tick(float dt)
        {
            
        }

        public void Exit()
        {
            _isActive.Value = false;

            _loadBreedsUseCase.Cancel();
            _loadBreedDetailsUseCase.Cancel();
            _breedPopupInfo.Close();
        }
    }
}
