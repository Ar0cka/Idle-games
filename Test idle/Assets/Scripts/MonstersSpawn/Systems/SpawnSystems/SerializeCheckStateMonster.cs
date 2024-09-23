using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.BattlePhase.Systems.BattleSystems.SpawnSystems
{
    public class SerializeCheckStateMonster : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<CheckStateMonster> _monsterStateFilter = null;

        public void Init()
        {
            var newEntity = _ecsWorld.NewEntity();

            newEntity.Get<CheckStateMonster>();
            
            foreach (var index in _monsterStateFilter)
            {
                ref var isMonsterAlive = ref _monsterStateFilter.Get1(index).isMonsterAlive;

                isMonsterAlive = false;
            }
        }
    }
}