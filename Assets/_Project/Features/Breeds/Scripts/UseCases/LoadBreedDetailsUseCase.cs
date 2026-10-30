using System;
using System.Threading;
using _Project.Features.Breeds.Scripts.Entities;
using _Project.Features.Breeds.Scripts.UseCases.Ports;
using _Project.Network.Scripts.Entities;
using Cysharp.Threading.Tasks;

namespace _Project.Features.Breeds.Scripts.UseCases
{
    public class LoadBreedDetailsUseCase
    {
        private readonly IRequestQueue _requestQueue;
        private readonly IBreedDetailsRequestFactory _requestFactory;
        private readonly BreedPopupInfo _breedPopupInfo;
        private readonly BreedsLoadState _loadState;
        
        private CancellationTokenSource _cts;

        public LoadBreedDetailsUseCase(
            IRequestQueue requestQueue,
            IBreedDetailsRequestFactory requestFactory,
            BreedPopupInfo breedPopupInfo,
            BreedsLoadState loadState)
        {
            _requestQueue = requestQueue;
            _requestFactory = requestFactory;
            _breedPopupInfo = breedPopupInfo;
            _loadState = loadState;
        }

        public void Load(string breedId)
        {
            if (_breedPopupInfo.IsOpened)
                return;
            
            if (_loadState.CurrentLoadingBreedId == breedId)
                return;
            
            CancelCurrent();
            _loadState.SetLoadingBreed(breedId);

            _cts = new CancellationTokenSource();
            LoadDetails(breedId, _cts.Token).Forget();
        }

        public void Cancel()
        {
            CancelCurrent();
            _loadState.SetLoadingBreed(null);
        }

        private async UniTaskVoid LoadDetails(string breedId, CancellationToken token)
        {
            try
            {
                BreedDetails details = await _requestQueue.Get(_requestFactory.Create(breedId), token);
                _breedPopupInfo.TryOpen(details);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception)
            {
                
            }

            FinishLoading(breedId);
        }

        private void FinishLoading(string breedId)
        {
            if (_loadState.CurrentLoadingBreedId == breedId)
                _loadState.SetLoadingBreed(null);
        }

        private void CancelCurrent()
        {
            if (_cts == null)
                return;

            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}
