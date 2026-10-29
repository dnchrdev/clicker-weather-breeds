using _Project.Features.Breeds.Scripts.Entities;
using _Project.Features.Breeds.Scripts.Infrastructure;
using _Project.Features.Breeds.Scripts.Infrastructure.BreedCard;
using _Project.Features.Breeds.Scripts.InterfaceInstaller;
using _Project.Features.Breeds.Scripts.InterfaceInstaller.Ports;
using _Project.Features.Breeds.Scripts.UseCases;
using _Project.Features.Breeds.Scripts.UseCases.Ports;
using _Project.Network.Scripts.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Features.Breeds.Scripts.Installer
{
    public class BreedsInstaller : MonoInstaller
    {
        [SerializeField] private BreedsView _breedsView;
        [SerializeField] private GameObject _breedCardPrefab;
        [SerializeField] private BreedsSettings _breedsSettings = new BreedsSettings();

        public override void InstallBindings()
        {
            Container.Bind<BreedsSettings>().FromInstance(_breedsSettings).AsSingle();

            Container.Bind<BreedsCatalog>().AsSingle();
            Container.Bind<BreedPopupInfo>().AsSingle();
            Container.Bind<BreedsLoadState>().AsSingle();

            Container.Bind<LoadBreedsUseCase>().AsSingle();
            Container.Bind<LoadBreedDetailsUseCase>().AsSingle();
            Container.Bind<CloseBreedDetailsPopupUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<BreedsTabState>().AsSingle();

            Container.BindInterfacesTo<BreedsController>().AsSingle();
            Container.BindInterfacesTo<BreedsPresenter>().AsSingle();

            Container.Bind<IRequest<Breed[]>>().To<BreedsListRequest>().AsSingle();
            Container.Bind<IBreedDetailsRequestFactory>().To<BreedDetailsRequestFactory>().AsSingle();
            Container.Bind<IBreedCardFactory>().To<BreedCardFactory>().AsSingle().WithArguments(_breedCardPrefab);

            Container.Bind<IBreedsView>().FromInstance(_breedsView).AsSingle();
        }
    }
}
