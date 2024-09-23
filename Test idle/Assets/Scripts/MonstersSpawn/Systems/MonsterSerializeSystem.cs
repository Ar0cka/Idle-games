using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.BattlePhase.Components.Events.SpawnEvents;
using DefaultNamespace.BattlePhase.Systems.BattleSystems.SpawnSystems;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.Components.BattleComponents
{
    public class MonsterSerializeSystem : IEcsRunSystem
    {
        private EcsFilter<CheckStateMonster> _monsterState;
        
        private EcsFilter<MonsterBattleComponents, MonsterCooldownAttackComponent> _monster = null;

        private EcsFilter<HpBarComponent> _hpBarComponent = null;
        
        public void Run()
        {
            foreach (var boolIndex in _monsterState)
            {
                ref var serializeMonsterState = ref _monsterState.Get1(boolIndex);
                
                Debug.Log($"Serialize bool = {serializeMonsterState.SerializeMonsterSettings}");
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