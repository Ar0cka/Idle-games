using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace MonsterSpawn.Systems.RespawnSystems
{
    public class RespawnMonsterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnSettings, RespawnMonsterEvent> _spawnFilter = null;
        private readonly EcsFilter<MonsterCheckStateComponent> _stateFilter = null;

        public void Run()
        {
            foreach (var spawnIndex in _spawnFilter)
            {
                ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);

                var monsterAlive = GetMonsterAlive();
                
                Debug.Log("Send choice monster event from respawn system");
                
                if (monsterAlive) continue;
                
                spawnEntity.Get<ChoiceMonsterFromListEvent>();
                spawnEntity.Del<RespawnMonsterEvent>();
            }
        }

        private bool GetMonsterAlive()
        {
            if (_stateFilter.GetEntitiesCount() > 0)
            {
                var monsterAlive = _stateFilter.Get1(0).MonsterAlive;
                return monsterAlive;
            }
            Debug.LogError("MonsterAlive don't find");
            return true;
        }
    }
}