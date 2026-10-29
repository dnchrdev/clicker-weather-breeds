using System;
using System.Collections.Generic;
using _Project.Features.Weather.Scripts.UseCases.Ports;
using UnityEngine;

namespace _Project.Features.Weather.Scripts.Infrastructure
{
    public class WeatherIconStorage : IWeatherIconStorage, IDisposable
    {
        private readonly Dictionary<string, Sprite> _icons = new Dictionary<string, Sprite>();

        public bool Has(string url)
        {
            return _icons.ContainsKey(url);
        }

        public bool TryGet(string url, out Sprite sprite)
        {
            return _icons.TryGetValue(url, out sprite);
        }

        public void Store(string url, byte[] data)
        {
            var texture = new Texture2D(2, 2);

            if (!texture.LoadImage(data))
            {
                UnityEngine.Object.Destroy(texture);
                throw new InvalidOperationException($"Decoding fail: {url}");
            }

            _icons[url] = Sprite.Create(
                texture,
                new Rect(0f, 0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
        }

        public void Dispose()
        {
            foreach (var sprite in _icons.Values)
            {
                if (sprite == null)
                    continue;

                UnityEngine.Object.Destroy(sprite.texture);
                UnityEngine.Object.Destroy(sprite);
            }

            _icons.Clear();
        }
    }
}
