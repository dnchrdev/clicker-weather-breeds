using System.Threading;
using _Project.Network.Scripts.Entities.Ports;
using Cysharp.Threading.Tasks;

namespace _Project.Network.Scripts.Entities
{
    public interface IRequestQueue
    {
        public UniTask<T> Get<T>(IRequest<T> request, CancellationToken token);
    }
    
    public class RequestQueue : IRequestQueue
    {
        private readonly IRequestExecutor _executor;
        private readonly SemaphoreSlim _slot = new SemaphoreSlim(1, 1);

        public RequestQueue(IRequestExecutor executor)
        {
            _executor = executor;
        }

        public async UniTask<T> Get<T>(IRequest<T> request, CancellationToken token)
        {
            byte[] bytes;
            
            await _slot.WaitAsync(token).AsUniTask();

            try
            {
                bytes = await _executor.Get(request.Url, request.Headers, token);
            }
            finally
            {
                _slot.Release();
            }
            
            token.ThrowIfCancellationRequested();
            return request.Parse(bytes);
        }
    }
}
