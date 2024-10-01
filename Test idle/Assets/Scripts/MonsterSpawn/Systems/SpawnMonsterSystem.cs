using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace MonsterSpawn.Systems
{
    public class SpawnMonsterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnSettings,  SpawnEvent> _spawnFilter;
        private readonly EcsFilter<MonsterForSpawnComponent> _monsterForSpawnFilter;
        private readonly EcsFilter<MonsterCheckStateComponent> _stateFilter;
        private readonly EcsFilter<SpawnCooldownComponent> _spawnCooldownFilter;

        public void Run()
        {
            foreach (var stateIndex in _stateFilter)
            {
                ref var stateSettings = ref _stateFilter.Get1(stateIndex);
                if (stateSettings.MonsterAlive) continue;

                foreach (var spawnIndex in _spawnFilter)
                {
                    ref var spawnSettings = ref _spawnFilter.Get1(spawnIndex);
                    ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);

                    foreach (var monsterIndex in _monsterForSpawnFilter)
                    {
                        ref var monsterObject = ref _monsterForSpawnFilter.Get1(monsterIndex);

                        foreach (var spawnCooldownIndex in _spawnCooldownFilter)
                        {
                            ref var spawnCooldown = ref _spawnCooldownFilter.Get1(spawnCooldownIndex).spawnBlockTimer;
                            ref var spawnCooldownEntity = ref _spawnCooldownFilter.GetEntity(spawnCooldownIndex);
                        
                            if (spawnCooldown <= 0)
                            {
                                spawnSettings.monsterSpawnScript.SpawnMonsterOnScene(monsterObject.monsterObject,
                                    spawnSettings.monsterPosition, spawnSettings.parent);
                            
                                Debug.Log("Spawn monster");

                                stateSettings.CanSerializeMonsterData = true;
                                stateSettings.MonsterAlive = true;
                                spawnCooldown = 3f;
                                spawnEntity.Del<SpawnEvent>();
                            }
                            else
                            {
                                spawnCooldownEntity.Get<SpawnCooldownEvent>();
                            }
                        }
                    }
                }
            }
        }
    }
}