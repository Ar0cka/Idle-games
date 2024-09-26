using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System.BattleSystems.MonstersAttackSystem
{
    public class SerializeMonsterCooldownSystem : IEcsRunSystem
    {
        private EcsFilter<MonsterCooldownAttackComponent, MonsterBattleComponents, SerializeAttackCooldownEvent> _attackSpeedEnemy = null;


        public void Run()
        {
            foreach (var attackIndex in _attackSpeedEnemy)
            {
                ref var attackSpeedEnemy = ref _attackSpeedEnemy.Get1(attackIndex).blockTimer;
                ref var monster = ref _attackSpeedEnemy.Get2(attackIndex);
                ref var monsterEntity = ref _attackSpeedEnemy.GetEntity(attackIndex);

                attackSpeedEnemy = monster.monstersAbstract.attackSpeed;
                monsterEntity.Del<SerializeAttackCooldownEvent>();
            }
        }
    }
}