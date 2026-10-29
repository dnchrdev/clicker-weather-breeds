using System.Collections.Generic;

namespace _Project.Network.Scripts.Entities
{
    public interface IRequest<T>
    {
        public string Url { get; }
        public IReadOnlyDictionary<string, string> Headers { get; }
        public T Parse(byte[] raw);
    }
}
