using _Project.Features.Breeds.Scripts.Entities;
using _Project.Features.Breeds.Scripts.UseCases.Ports;
using _Project.Network.Scripts.Entities;

namespace _Project.Features.Breeds.Scripts.Infrastructure
{
    public class BreedDetailsRequestFactory : IBreedDetailsRequestFactory
    {
        private readonly BreedsSettings _config;

        public BreedDetailsRequestFactory(BreedsSettings config)
        {
            _config = config;
        }

        public IRequest<BreedDetails> Create(string breedId)
        {
            return new BreedDetailsRequest(_config, breedId);
        }
    }
}
