using System;

namespace _Project.Features.Breeds.Scripts.Infrastructure
{
    [Serializable]
    public class BreedAttributesDto
    {
        public string name;
        public string description;
    }

    [Serializable]
    public class BreedDto
    {
        public string id;
        public BreedAttributesDto attributes;
    }

    [Serializable]
    public class BreedsResponseDto
    {
        public BreedDto[] data;
    }

    [Serializable]
    public class BreedResponseDto
    {
        public BreedDto data;
    }
}
