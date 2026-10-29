using _Project.Network.Scripts.Entities;
using _Project.Network.Scripts.Entities.Ports;
using _Project.Network.Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Network.Scripts.Installer
{
    public class NetworkInstaller : MonoInstaller
    {
        [SerializeField] private NetworkSettings _networkSettings = new NetworkSettings();

        public override void InstallBindings()
        {
            Container.Bind<NetworkSettings>().FromInstance(_networkSettings).AsSingle();

            Container.Bind<IRequestExecutor>().To<UnityWebRequestExecutor>().AsSingle();
            Container.BindInterfacesTo<RequestQueue>().AsSingle();
        }
    }
}
