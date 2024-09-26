using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.BattlePhase.Components.Events.SpawnEvents;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace DefaultNamespace.BattlePhase.Systems.BattleSystems.SpawnSystems
{
    public class SpawnMonsterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MonsterSpawnComponent, SpawnMonsterEvent> _monsterFilter;
        
        private readonly EcsFilter<CheckStateMonster> _monsterStateFilter;

        private readonly EcsFilter<SpawnSettings> _spawnFilter;
        
        private readonly EcsFilter<MonsterBattleComponents> _monsterDataFilter = null;

        public void Run()
        {
            foreach (var monsterIndex in _monsterFilter)
            {
                ref var _monsterPrefab = ref _monsterFilter.Get1(monsterIndex);
                ref var entity = ref _monsterFilter.GetEntity(monsterIndex);
                
                foreach (var boolIndex in _monsterStateFilter) 
                { 
                    ref var isMonsterAlive = ref _monsterStateFilter.Get1(boolIndex);
                    
                    if (isMonsterAlive.isMonsterAlive) continue;

                    foreach (var swapIndex in _spawnFilter)
                    {
                        Debug.Log($"Monster alive = {isMonsterAlive.isMonsterAlive}");
                        
                        ref var spawnSettings = ref _spawnFilter.Get1(swapIndex);
                        
                        spawnSettings._spawnMonster.SpawnMonsterToScene(_monsterPrefab._monster, spawnSettings._spawPosition
                        , spawnSettings._parent.transform);

                        isMonsterAlive.SerializeMonsterSettings = true;
                        isMonsterAlive.isMonsterAlive = true;
                    }
                    
                    entity.Del<SpawnMonsterEvent>();
                }
            }
        }
    }
}