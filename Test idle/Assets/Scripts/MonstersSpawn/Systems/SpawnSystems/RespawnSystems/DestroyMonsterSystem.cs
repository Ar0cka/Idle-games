using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.BattlePhase.Components.Events.SpawnEvents;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.BattlePhase.Systems.BattleSystems.SpawnSystems
{
    public class DestroyMonsterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnSettings, DestroyMonsterEvent> _spawnEcsFilter = null;
        private readonly EcsFilter<CheckStateMonster> _monsterStateFilter = null;
        private readonly EcsFilter<HpBarComponent> _hpBarFilter = null;

        public void Run()
        {
            foreach (var spawnIndex in _spawnEcsFilter)
            {
                ref var spawnSettings = ref _spawnEcsFilter.Get1(spawnIndex);
                ref var entity = ref _spawnEcsFilter.GetEntity(spawnIndex);
                
                spawnSettings._spawnMonster.DestroyMonster();

                foreach (var stateIndex in _monsterStateFilter)
                {
                    ref var stateMonster = ref _monsterStateFilter.Get1(stateIndex).isMonsterAlive;
                    ref var stateEntity = ref _monsterStateFilter.GetEntity(stateIndex);
                    
                    stateMonster = false;
                    stateEntity.Get<HideHpBarEnemyEvent>();
                }
                
                entity.Del<DestroyMonsterEvent>();
            }
        }
    }
}