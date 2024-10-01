
using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.AttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Components;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace BattlePhase.Systems.BattleSystems.PlayerAttackSystem
{
    public class PlayerTakeDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerSettingsComponent, MonsterAttackEvent> _player = null;
        private readonly EcsFilter<HpBarComponent> _hpBarFilter = null;

        public void Run()
        {
            foreach (var i in _player)
            {
                ref var player = ref _player.Get1(i);
                ref var damageMonster = ref _player.Get2(i).damageMonster;
                ref var entity = ref _player.GetEntity(i);

                var barEntity = _hpBarFilter.GetEntitiesCount() > 0 ? _hpBarFilter.GetEntity(0) : default;
                
                if (damageMonster > 0)
                {
                    player.currentHP -= damageMonster;
                    
                    if (barEntity != default)
                    barEntity.Get<UpdatePlayerUIEvent>();
                    
                    entity.Del<MonsterAttackEvent>();
                }
            }
        }
    }
}