using _Project.Features.Clicker.Scripts.Entities;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Infrastructure
{
    [CreateAssetMenu(fileName = "ClickerConfig", menuName = "Configs/Clicker")]
    public sealed class ClickerConfig : ScriptableObject
    {
        [SerializeField] private EnergySettings _energy = new EnergySettings();
        [SerializeField] private RewardSettings _rewards = new RewardSettings();
        [SerializeField] private AutoCollectSettings _autoCollect = new AutoCollectSettings();

        public EnergySettings Energy => _energy;
        public RewardSettings Rewards => _rewards;
        public AutoCollectSettings AutoCollect => _autoCollect;

        private void OnValidate()
        {
            _energy.Max = Mathf.Max(1, _energy.Max);
            _energy.Start = Mathf.Clamp(_energy.Start, 0, _energy.Max);
            _energy.RegenIntervalSeconds = Mathf.Max(0.1f, _energy.RegenIntervalSeconds);
            _autoCollect.IntervalSeconds = Mathf.Max(0.1f, _autoCollect.IntervalSeconds);
        }
    }
}