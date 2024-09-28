using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.BattlePhase.Components.Events.SpawnEvents;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.BattlePhase.Systems.BattleSystems.SpawnSystems
{
    public class BlockSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnSettings, BlockSpawnEvent> _spawnFilter = null;

        public void Run()
        {
            foreach (var spawnIndex in _spawnFilter)
            {
                ref var spawnSettings = ref _spawnFilter.Get1(spawnIndex);
                ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);

                spawnSettings.blockSpawnTimer -= Time.deltaTime;

                //Debug.Log($"Block timer = {spawnSettings.blockSpawnTimer}");
                
                if (spawnSettings.blockSpawnTimer <= 0)
                {
                    spawnEntity.Del<BlockSpawnEvent>();
                }
            }
        }
    }
}