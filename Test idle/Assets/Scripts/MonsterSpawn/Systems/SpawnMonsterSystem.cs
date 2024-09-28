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
            throw new System.NotImplementedException();
        }
    }
}