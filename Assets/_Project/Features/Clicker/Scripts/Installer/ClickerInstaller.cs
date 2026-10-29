using _Project.Features.Clicker.Scripts.Entities;
using _Project.Features.Clicker.Scripts.Infrastructure;
using _Project.Features.Clicker.Scripts.InterfaceAdapters;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Clicker.Scripts.UseCases;
using UnityEngine;
using Zenject;

namespace _Project.Features.Clicker.Scripts.Installer
{
    public class ClickerInstaller: MonoInstaller
    {
        [SerializeField] private ClickerView _clickerView;
        [SerializeField] private RewardPopupView _rewardPopupView;
        [SerializeField] private ClickerConfig  _clickerConfig;
        [SerializeField] private GameObject _popupPrefab;
        [SerializeField] private Transform _showedPopupsContainer;
        [SerializeField] private Transform _hiddenPopupsContainer;
        
        override public void InstallBindings()
        {
            Container.BindInterfacesTo<Energy>().AsSingle();
            Container.Bind<Wallet>().AsSingle();
            Container.BindInterfacesTo<AutoCollectAutoCollectIntervalTimer>().AsSingle();
            
            Container.Bind<AutoCollectUseCase>().AsSingle();
            Container.Bind<TapUseCase>().AsSingle();
            Container.Bind<RewardPopupUseCase>().AsSingle();
            Container.Bind<OuterRewardUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClickerTabState>().AsSingle();
            
            Container.BindInterfacesTo<ClickerController>().AsSingle();
            Container.BindInterfacesTo<ClickerPresenter>().AsSingle();
            Container.BindInterfacesTo<RewardPopupController>().AsSingle();
            Container.BindInterfacesTo<RewardPopupPresenter>().AsSingle();
            
            Container.Bind<IClickerView>().FromInstance(_clickerView).AsSingle();
            
            Container.BindInterfacesTo<RewardPopupFactory>().AsSingle().WithArguments(_popupPrefab);
            Container.BindInterfacesTo<RewardPopupView>().FromInstance(_rewardPopupView).AsSingle();
            
            Container.Bind<AutoCollectSettings>().FromInstance(_clickerConfig.AutoCollect).AsSingle();
            Container.Bind<EnergySettings>().FromInstance(_clickerConfig.Energy).AsSingle();
            Container.Bind<RewardSettings>().FromInstance(_clickerConfig.Rewards).AsSingle();
        }
    }
}
