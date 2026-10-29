using System;
using System.Collections.Generic;
using System.Text;
using _Project.Features.Breeds.Scripts.Entities;
using _Project.Network.Scripts.Entities;
using UnityEngine;

namespace _Project.Features.Breeds.Scripts.Infrastructure
{
    public class BreedDetailsRequest : IRequest<BreedDetails>
    {
        private readonly Dictionary<string, string> _headers;

        public string Url { get; }
        public IReadOnlyDictionary<string, string> Headers => _headers;

        public BreedDetailsRequest(BreedsSettings config, string breedId)
        {
            Url = $"{config.BreedsUrl}/{breedId}";

            _headers = new Dictionary<string, string>
            {
                { "User-Agent", config.UserAgent },
                { "Accept", "application/json" }
            };
        }

        public BreedDetails Parse(byte[] raw)
        {
            var response = JsonUtility.FromJson<BreedResponseDto>(Encoding.UTF8.GetString(raw));
            var dto = response?.data;

            if (dto?.attributes == null)
                throw new FormatException("Breeds not found");

            return new BreedDetails(dto.id, dto.attributes.name, dto.attributes.description);
        }
    }
}
