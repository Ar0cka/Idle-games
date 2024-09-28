
using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.AttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace BattlePhase.Systems.BattleSystems.PlayerAttackSystem
{
    public class PlayerTakeDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerSettingsComponent, MonsterAttackEvent> _player = null;

        public void Run()
        {
            foreach (var i in _player)
            {
                ref var player = ref _player.Get1(i);
                ref var damageMonster = ref _player.Get2(i).damageMonster;
                ref var entity = ref _player.GetEntity(i);

                if (damageMonster > 0)
                {
                    player.currentHP -= damageMonster;
                    
                    entity.Get<UpdateUIEvent>();
                    entity.Del<MonsterAttackEvent>();
                }
            }
        }
    }
}