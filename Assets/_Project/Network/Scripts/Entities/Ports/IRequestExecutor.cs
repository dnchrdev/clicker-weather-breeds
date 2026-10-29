using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Network.Scripts.Entities.Ports
{
    public interface IRequestExecutor
    {
        public UniTask<byte[]> Get(string url, IReadOnlyDictionary<string, string> headers, CancellationToken token);
    }
}
