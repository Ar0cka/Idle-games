using System;
using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters.MonoBehavior;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.MonstersSpawn.Systems
{
    public class MonsterSerializeDataSystem : IEcsRunSystem
    {
        private EcsFilter<CheckStateMonster> _monsterState;
        
        private EcsFilter<MonsterBattleComponents, MonsterCooldownAttackComponent> _monster = null;

        private EcsFilter<HpBarComponent> _hpBarComponent = null;
        
        public void Run()
        {
            foreach (var boolIndex in _monsterState)
            {
                ref var serializeMonsterState = ref _monsterState.Get1(boolIndex);

                if (serializeMonsterState.SerializeMonsterSettings)
                {
                    Debug.Log($"Serialize bool = true");
                }
              
                if (!serializeMonsterState.SerializeMonsterSettings) continue;
                
                MonsterSerializeMethod();

                OnHpBarEnemy();
                
                serializeMonsterState.SerializeMonsterSettings = false;
            }
        }

        private void OnHpBarEnemy()
        {
            var barEntity = _hpBarComponent.GetEntitiesCount() > 0 ? _hpBarComponent.GetEntity(0) : default;

            if (barEntity != default)
                barEntity.Get<OnHpBarEnemyEvent>();
        }

        private void MonsterSerializeMethod()
        {
            foreach (var i in _monster)
            {
                ref var monster = ref _monster.Get1(i);
                ref var attackSpeedEnemy = ref _monster.Get2(i).blockTimer;
                ref var entity = ref _monster.GetEntity(i);

                monster.currentHP = monster.monstersAbstract._hitPointMonster;
                Debug.Log($"Monster serialize hp = {monster.currentHP}");
                attackSpeedEnemy = monster.monstersAbstract._attackSpeed;
                Debug.Log($"Monster attack speed = {attackSpeedEnemy}");
                
                entity.Get<UpdateUIEvent>();
            }
        }
    }
}