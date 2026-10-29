using System;
using System.Threading;
using _Project.Features.Breeds.Scripts.Entities;
using _Project.Network.Scripts.Entities;
using Cysharp.Threading.Tasks;

namespace _Project.Features.Breeds.Scripts.UseCases
{
    public class LoadBreedsUseCase
    {
        private readonly IRequestQueue _requestQueue;
        private readonly IRequest<Breed[]> _breedsRequest;
        private readonly BreedsSettings _config;
        private readonly BreedsCatalog _breedsCatalog;
        private readonly BreedsLoadState _loadState;
        
        private CancellationTokenSource _cts;

        public LoadBreedsUseCase(
            IRequestQueue requestQueue,
            IRequest<Breed[]> breedsRequest,
            BreedsSettings config,
            BreedsCatalog breedsCatalog,
            BreedsLoadState loadState)
        {
            _requestQueue = requestQueue;
            _breedsRequest = breedsRequest;
            _config = config;
            _breedsCatalog = breedsCatalog;
            _loadState = loadState;
        }

        public void Load()
        {
            CancelCurrent();
            _loadState.SetListLoading(true);

            _cts = new CancellationTokenSource();
            LoadList(_cts.Token).Forget();
        }

        public void Cancel()
        {
            CancelCurrent();
            _loadState.SetListLoading(false);
        }

        private async UniTaskVoid LoadList(CancellationToken token)
        {
            try
            {
                Breed[] breeds = await _requestQueue.Get(_breedsRequest, token);
                _breedsCatalog.TryUpdate(TakeShowed(breeds));
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception)
            {
            }

            _loadState.SetListLoading(false);
        }

        private Breed[] TakeShowed(Breed[] breeds)
        {
            int count = Math.Min(Math.Max(1, _config.ShowCardsCount), breeds.Length);
            var showed = new Breed[count];

            Array.Copy(breeds, showed, count);

            return showed;
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
