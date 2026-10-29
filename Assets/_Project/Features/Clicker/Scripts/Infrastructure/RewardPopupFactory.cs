using _Project.Features.Clicker.Scripts.Infrastructure.RewardPopup;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Infrastructure
{
    public class RewardPopupFactory : IRewardPopupFactory
    {
        private readonly GameObject _prefab;

        public RewardPopupFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public IRewardPopup Create(Transform parent)
        {
            var go = UnityEngine.Object.Instantiate(_prefab, parent);
            return go.GetComponent<IRewardPopup>();
        }
    }
}
