using System;
using System.Collections.Generic;
using System.Text;
using _Project.Features.Breeds.Scripts.Entities;
using _Project.Network.Scripts.Entities;
using UnityEngine;

namespace _Project.Features.Breeds.Scripts.Infrastructure
{
    public class BreedsListRequest : IRequest<Breed[]>
    {
        private readonly BreedsSettings _config;
        private readonly Dictionary<string, string> _headers;

        public string Url => _config.BreedsUrl;
        public IReadOnlyDictionary<string, string> Headers => _headers;

        public BreedsListRequest(BreedsSettings config)
        {
            _config = config;

            _headers = new Dictionary<string, string>
            {
                { "User-Agent", config.UserAgent },
                { "Accept", "application/json" }
            };
        }

        public Breed[] Parse(byte[] raw)
        {
            var response = JsonUtility.FromJson<BreedsResponseDto>(Encoding.UTF8.GetString(raw));

            if (response?.data == null)
                throw new FormatException("Breeds not found");

            var breeds = new Breed[response.data.Length];

            for (int i = 0; i < breeds.Length; i++)
            {
                var dto = response.data[i];
                breeds[i] = new Breed(dto?.id, dto?.attributes?.name);
            }

            return breeds;
        }
    }
}
