using _Project.Features.Breeds.Scripts.Infrastructure;
using _Project.Features.Breeds.Scripts.InterfaceInstaller;
using _Project.Features.Breeds.Scripts.InterfaceInstaller.Ports;
using _Project.Features.Breeds.Scripts.UseCases;
using UnityEngine;
using Zenject;

namespace _Project.Features.Breeds.Scripts.Installer
{
    public class BreedsInstaller : MonoInstaller
    {
        [SerializeField] private BreedsView _breedsView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BreedsTabState>().AsSingle();

            Container.BindInterfacesTo<BreedsPresenter>().AsSingle();

            Container.Bind<IBreedsView>().FromInstance(_breedsView).AsSingle();
        }
    }
}