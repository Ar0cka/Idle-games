using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System.BattleSystems.MonstersAttackSystem
{
    public class EnemyBlockSystem : IEcsRunSystem
    {
        private EcsFilter<MonsterCooldownAttackComponent, EnemyBlockAttackEvent> _monsterBattle = null;

        public void Run()
        {
            foreach (var i in _monsterBattle)
            {
                ref var cooldownTimer = ref _monsterBattle.Get1(i).blockTimer;
                ref var entity = ref _monsterBattle.GetEntity(i);
                
                cooldownTimer -= Time.deltaTime;

                if (cooldownTimer <= 0)
                {
                    entity.Del<EnemyBlockAttackEvent>();
                }
            }
        }
    }
}