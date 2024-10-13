using DefaultNamespace.UpgradeMenu.Event;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.UpgradeMenu.Systems
{
    public class UpgradeHPPlayer : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private PlayerSettings _playerSettings;
        private readonly EcsFilter<UpStatsPlayerEvent> _statesFilter = null;
        
        public void Run()
        {
            Debug.Log($"States entity = {_statesFilter.GetEntitiesCount()}")
            ;
            foreach (var statesIndex in _statesFilter)
            {
                ref var states = ref _statesFilter.Get1(statesIndex).upgradeEmpty;
                ref var statesEntity = ref _statesFilter.GetEntity(statesIndex);
                
                switch (states.statesType)
                {
                    case StatesType.Health : 
                        _playerSettings.UpgradeHitPoint((int) states.upgradeFloatStats);
                        break;
                    case StatesType.Armour :
                        _playerSettings.UpgradeArmour(states.upgradeFloatStats);
                        break;
                    case StatesType.AttackSpeed :
                        _playerSettings.UpgradeAttackSpeed(states.upgradeFloatStats);
                        break;
                    case StatesType.Critical :
                        break;
                    case StatesType.CriticalChance :
                        break;
                    case StatesType.Damage :
                        _playerSettings.UpgradeDamage(states.upgradeFloatStats);
                        break;
                }
                
                statesEntity.Destroy();
            }
        }
    }
} 