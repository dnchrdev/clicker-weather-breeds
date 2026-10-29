using System.Collections.Generic;
using System.Threading;
using _Project.Network.Scripts.Entities;
using _Project.Network.Scripts.Entities.Ports;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _Project.Network.Scripts.Infrastructure
{
    public class UnityWebRequestExecutor : IRequestExecutor
    {
        private readonly NetworkSettings _settings;

        public UnityWebRequestExecutor(NetworkSettings settings)
        {
            _settings = settings;
        }

        public async UniTask<byte[]> Get(
            string url,
            IReadOnlyDictionary<string, string> headers,
            CancellationToken token)
        {
            using (var request = UnityWebRequest.Get(url))
            {
                request.timeout = _settings.TimeoutSeconds;

                if (headers != null)
                {
                    foreach (var header in headers)
                        request.SetRequestHeader(header.Key, header.Value);
                }
                
                try
                {
                    await request.SendWebRequest().ToUniTask(cancellationToken: token);
                }
                catch (UnityWebRequestException exception)
                {
                    Debug.LogWarning($"[Network] {exception.Message} ({url})");
                    throw;
                }

                return request.downloadHandler.data;
            }
        }
    }
}
