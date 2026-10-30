using _Project.Features.Clicker.Scripts.Entities;
using _Project.Features.Clicker.Scripts.Infrastructure;
using _Project.Features.Clicker.Scripts.Infrastructure.RewardPopup;
using _Project.Features.Clicker.Scripts.Infrastructure.TapEffects;
using _Project.Features.Clicker.Scripts.InterfaceAdapters;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Clicker.Scripts.UseCases;
using _Project.Features.Clicker.Scripts.UseCases.Ports;
using UnityEngine;
using Zenject;

namespace _Project.Features.Clicker.Scripts.Installer
{
    public class ClickerInstaller: MonoInstaller
    {
        [SerializeField] private ClickerView _clickerView;
        [SerializeField] private RewardPopupView _rewardPopupView;
        [SerializeField] private TapEffectView _tapEffectView;
        [SerializeField] private TapSfx _tapSfx;
        [SerializeField] private GameObject _tapEffectPrefab;
        [SerializeField] private ClickerConfig  _clickerConfig;
        [SerializeField] private GameObject _popupPrefab;
        
        override public void InstallBindings()
        {
            Container.BindInterfacesTo<Energy>().AsSingle();
            Container.Bind<Wallet>().AsSingle();
            Container.BindInterfacesTo<AutoCollectAutoCollectIntervalTimer>().AsSingle();
            
            Container.Bind<AutoCollectUseCase>().AsSingle();
            Container.Bind<TapUseCase>().AsSingle();
            Container.Bind<OuterRewardUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClickerTabState>().AsSingle();
            
            Container.BindInterfacesTo<ClickerController>().AsSingle();
            Container.BindInterfacesTo<ClickerPresenter>().AsSingle();
            Container.BindInterfacesTo<RewardPopupController>().AsSingle();
            
            Container.Bind<IClickerView>().FromInstance(_clickerView).AsSingle();
            Container.Bind<ITapButtonPunch>().FromInstance(_clickerView).AsSingle();
            
            Container.BindInterfacesTo<RewardPopupFactory>().AsSingle().WithArguments(_popupPrefab);
            Container.BindInterfacesTo<RewardPopupView>().FromInstance(_rewardPopupView).AsSingle();
            
            Container.BindInterfacesTo<TapEffectFactory>().AsSingle().WithArguments(_tapEffectPrefab);
            Container.BindInterfacesTo<TapEffectView>().FromInstance(_tapEffectView).AsSingle();
            Container.Bind<ITapSfx>().FromInstance(_tapSfx).AsSingle();
            
            Container.Bind<AutoCollectSettings>().FromInstance(_clickerConfig.AutoCollect).AsSingle();
            Container.Bind<EnergySettings>().FromInstance(_clickerConfig.Energy).AsSingle();
            Container.Bind<RewardSettings>().FromInstance(_clickerConfig.Rewards).AsSingle();
        }
    }
}
