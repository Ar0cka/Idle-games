using System;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters.MonoBehavior;
using DefaultNamespace.Battle.Components.MonsterComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.MonstersSpawn.Systems
{
    public class MonsterSerializeDataSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<MonsterBattleComponents, MonsterCooldownAttackComponent> _monsterDataFilter = null;
        private readonly EcsFilter<CheckStateMonster> _stateFilter = null;
        private readonly EcsFilter<SpawnSettings> _spawnFilter = null;

        public void Run()
        {
            var canSerialize = ReturnSerializeData();
            var monsterSpawn = ReturnSpawnComponent();
            
            var monsterWorld = _ecsWorld.NewEntity();
            monsterWorld.Get<MonsterBattleComponents>();


        }

        private SpawnSettings ReturnSpawnComponent()
        {
            var monsterSpawn = _spawnFilter.GetEntitiesCount() > 0 ? _spawnFilter.Get1(0) : default;

            try
            {
                return monsterSpawn;
            }
            catch (Exception e)
            {
                Debug.LogError($"failed monster spawn serialize {e}");
                throw;
            }
        }
        private bool ReturnSerializeData()
        {
            var stateMonster = _stateFilter.GetEntitiesCount() > 0 ? _stateFilter.Get1(0).SerializeMonsterSettings : default;

            try
            {
                return stateMonster;
            }
            catch (Exception e)
            {
                Debug.LogError($"Serialize state monster failed {e}");
                return false;
            }
        }
    }
}