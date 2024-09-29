using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;

namespace MonsterSpawn.Systems
{
    public class SpawnMonsterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnSettings, SpawnEvent> _spawnFilter;
        private readonly EcsFilter<MonsterForSpawnComponent> _monsterForSpawnFilter;
        private readonly EcsFilter<MonsterCheckStateComponent> _stateFilter;

        public void Run()
        {
            foreach (var stateIndex in _stateFilter)
            {
                ref var stateSettings = ref _stateFilter.Get1(stateIndex);
                if (!stateSettings.MonsterAlive) continue;

                foreach (var spawnIndex in _spawnFilter)
                {
                    ref var spawnSettings = ref _spawnFilter.Get1(spawnIndex);
                    ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);

                    foreach (var monsterIndex in _monsterForSpawnFilter)
                    {
                        ref var monsterObject = ref _monsterForSpawnFilter.Get1(monsterIndex);
                        spawnSettings.monsterSpawnScript.SpawnMonsterOnScene(monsterObject.monsterObject, 
                            spawnSettings.monsterPosition, spawnSettings.parent);
                        spawnEntity.Del<SpawnEvent>();
                    }
                }
            }
        }
    }
}