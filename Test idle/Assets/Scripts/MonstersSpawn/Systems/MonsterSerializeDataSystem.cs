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
            var monsterState = ReturnSerializeData();
            var monsterSpawn = ReturnSpawnComponent();
            
            var monsterWorld = _ecsWorld.NewEntity();
            monsterWorld.Get<MonsterBattleComponents>().monstersAbstract = monsterSpawn._spawnMonster.GetMonsterData();
            monsterWorld.Get<MonsterCooldownAttackComponent>();

            if (monsterState.SerializeMonsterSettings)
            {
                foreach (var monsterIndex in _monsterDataFilter)
                {
                    ref var monster = ref _monsterDataFilter.Get1(monsterIndex);
                    ref var monsterAttackCooldown = ref _monsterDataFilter.Get2(monsterIndex);

                    monster.currentHP = monster.monstersAbstract.hitPoint;
                    monsterAttackCooldown.blockTimer = monster.monstersAbstract.attackSpeed;

                    monsterState.SerializeMonsterSettings = false;
                }
            }
            
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
        private CheckStateMonster ReturnSerializeData()
        {
            var stateMonster = _stateFilter.GetEntitiesCount() > 0 ? _stateFilter.Get1(0) : default;

            try
            {
                return stateMonster;
            }
            catch (Exception e)
            {
                Debug.LogError($"Serialize state monster failed {e}");
                throw;
            }
        }
    }
}