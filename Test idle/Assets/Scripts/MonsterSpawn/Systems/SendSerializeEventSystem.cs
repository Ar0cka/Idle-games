using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace MonsterSpawn.Systems
{
    public class SendSerializeEventSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnSettings> _spawnFilter = null;
        private readonly EcsFilter<MonsterCheckStateComponent> _stateFilter = null;

        public void Run()
        {
            foreach (var stateIndex in _stateFilter)
            {
                ref var stateMonster = ref _stateFilter.Get1(stateIndex);
                
                if (stateMonster.CanSerializeMonsterData & stateMonster.MonsterAlive) continue;

                foreach (var spawnIndex in _spawnFilter)
                {
                    ref var spawnSettings = ref _spawnFilter.Get1(spawnIndex);
                    ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);
                    
                    var monsterObject = spawnSettings.monsterSpawnScript.GetMonsterFromScene();
                        
                    if (monsterObject != null)
                        spawnEntity.Get<SerializeMonsterEvent>();
                }
            }
        }
    }
}