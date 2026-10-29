using _Project.Features.Breeds.Scripts.Entities;
using _Project.Network.Scripts.Entities;

namespace _Project.Features.Breeds.Scripts.UseCases.Ports
{
    public interface IBreedDetailsRequestFactory
    {
        public IRequest<BreedDetails> Create(string breedId);
    }
}
