using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.AttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System.BattleSystems.MonstersAttackSystem
{
    sealed class EnemyTakeDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MonsterBattleComponents, PlayerAttackEvent> _monsterFilter = null;

        public void Run()
        {
            foreach (var i in _monsterFilter)
            {
                ref var monster = ref _monsterFilter.Get1(i);
                ref var attackEvent = ref _monsterFilter.Get2(i);
                ref var entity = ref _monsterFilter.GetEntity(i);

                if (attackEvent.damagePlayer > 0)
                {
                    monster.currentHP -= attackEvent.damagePlayer;
                    attackEvent.damagePlayer = 0;
                    entity.Get<UpdateUIEvent>();
                    entity.Del<PlayerAttackEvent>();
                }
            }
        }
        
    }
}