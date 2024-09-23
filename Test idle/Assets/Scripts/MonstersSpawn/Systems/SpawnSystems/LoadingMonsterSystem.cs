using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.BattlePhase.Components.Events.SpawnEvents;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.BattlePhase.Systems.BattleSystems.SpawnSystems
{
    public class LoadingMonsterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MonsterSpawnComponent, LoadingMonsterEvent> _monsterFilter = null;
        private readonly EcsFilter<CheckStateMonster> _monsterAliveFilter;
   

        public void Run()
        {
            foreach (var monsterIndex in _monsterFilter)
            {
                ref var monster = ref _monsterFilter.Get1(monsterIndex);
                ref var loadingPrefab = ref _monsterFilter.Get2(monsterIndex);
                ref var entity = ref _monsterFilter.GetEntity(monsterIndex);

                monster._monster = loadingPrefab._prefab;
                entity.Del<LoadingMonsterEvent>();
                    
                entity.Get<SpawnMonsterEvent>();
                
                Debug.Log($"Loading monster complit. Game object = {monster._monster}");
            }
        }
    }
}