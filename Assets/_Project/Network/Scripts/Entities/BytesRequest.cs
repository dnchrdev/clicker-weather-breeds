using System.Collections.Generic;

namespace _Project.Network.Scripts.Entities
{
    public class BytesRequest : IRequest<byte[]>
    {
        private static readonly IReadOnlyDictionary<string, string> NoHeaders = new Dictionary<string, string>();

        public string Url { get; }
        public IReadOnlyDictionary<string, string> Headers => NoHeaders;

        public BytesRequest(string url)
        {
            Url = url;
        }

        public byte[] Parse(byte[] raw)
        {
            return raw;
        }
    }
}
