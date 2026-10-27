using System;
using UniRx;

namespace _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports
{
    public interface IClickerView
    {
        public IObservable<Unit> TapClicked { get; }
         
        public void ShowView();
        public void HideView();
        public void SubscribeToButtons();
        public void UnsubscribeToButtons();
        
        
    }
}