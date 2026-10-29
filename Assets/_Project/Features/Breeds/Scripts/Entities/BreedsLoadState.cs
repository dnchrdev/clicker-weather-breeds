using System;
using UniRx;

namespace _Project.Features.Breeds.Scripts.Entities
{
    public class BreedsLoadState
    {
        private readonly ReactiveProperty<bool> _listLoading = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<string> _loadingBreedId = new ReactiveProperty<string>();

        public IObservable<bool> ListLoading => _listLoading;
        public IObservable<string> LoadingBreedId => _loadingBreedId;

        public string CurrentLoadingBreedId => _loadingBreedId.Value;

        public void SetListLoading(bool isLoading)
        {
            _listLoading.Value = isLoading;
        }
        
        public void SetLoadingBreed(string breedId)
        {
            _loadingBreedId.Value = breedId;
        }
    }
}
