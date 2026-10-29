using System;
using UniRx;

namespace _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports
{
    public interface IClickerView
    {
        public IObservable<Unit> TapClicked { get; }
        public void UpdateWallet(int amount);
        public void UpdateEnergy(int amount);
        public void UpdateEnergyFill(float ratio);
        public void ShowView();
        public void HideView();
        public void SubscribeToButtons();
        public void UnsubscribeToButtons();
        
        
    }
}