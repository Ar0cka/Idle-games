using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace MonsterSpawn.Systems
{
    public class BLockSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnCooldownComponent, SpawnCooldownEvent> _spawnFilter = null;
        
        
        public void Run()
        {
            foreach (var spawnIndex in _spawnFilter)
            {
                ref var spawnSettings = ref _spawnFilter.Get1(spawnIndex);
                ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);

                spawnSettings.spawnBlockTimer -= Time.deltaTime;
                
                if (spawnSettings.spawnBlockTimer <= 0)
                {
                    spawnEntity.Del<SpawnCooldownEvent>();
                }
            }
        }
    }
}