using _Project.Features.Clicker.Scripts.Infrastructure.RewardPopup;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports
{
    public interface IRewardPopupFactory
    {
        public IRewardPopup Create(Transform parent);
    }
}
