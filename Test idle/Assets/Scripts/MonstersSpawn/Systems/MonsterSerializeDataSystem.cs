using System;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters.MonoBehavior;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.BattlePhase.Components.Events.SpawnEvents;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.MonstersSpawn.Systems
{
    public class MonsterSerializeDataSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<MonsterBattleComponents, MonsterCooldownAttackComponent, RespawnEvent> _monsterDataFilter = null;
        private readonly EcsFilter<CheckStateMonster> _stateFilter = null;
        private readonly EcsFilter<SpawnSettings> _spawnFilter = null;
    

        public void Run()
        {
            var monsterSpawn = ReturnSpawnComponent();
                
            var monsterWorld = _ecsWorld.NewEntity(); 
            monsterWorld.Get<MonsterBattleComponents>().monstersAbstract = monsterSpawn._spawnMonster.GetMonsterData(); 
            monsterWorld.Get<MonsterCooldownAttackComponent>();
                
                
            foreach (var monsterIndex in _monsterDataFilter)
            {
                ref var monster = ref _monsterDataFilter.Get1(monsterIndex);
                ref var monsterAttackCooldown = ref _monsterDataFilter.Get2(monsterIndex);
                    
                monster.currentHP = monster.monstersAbstract.hitPoint;
                monsterAttackCooldown.blockTimer = monster.monstersAbstract.attackSpeed;
                    
                Debug.Log($"Monster hp = {monster.currentHP} and block time = {monsterAttackCooldown.blockTimer}"); 
            }
            
        }

        private SpawnSettings ReturnSpawnComponent()
        {
            if (_spawnFilter.GetEntitiesCount() > 0)
            {
                return _spawnFilter.Get1(0);
            }
            
            Debug.LogError($"failed monster spawn serialize");
            return default;
        }
    }
}