using _Project.Features.Breeds.Scripts.Entities;

namespace _Project.Features.Breeds.Scripts.UseCases
{
    public class CloseBreedDetailsPopupUseCase
    {
        private readonly BreedPopupInfo _breedPopupInfo;

        public CloseBreedDetailsPopupUseCase(BreedPopupInfo breedPopupInfo)
        {
            _breedPopupInfo = breedPopupInfo;
        }

        public void Close()
        {
            _breedPopupInfo.Close();
        }
    }
}
