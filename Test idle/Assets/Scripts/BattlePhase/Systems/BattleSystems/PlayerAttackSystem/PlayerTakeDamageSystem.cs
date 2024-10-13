
using System;
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
        private PlayerData _playerData;

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
                    var effectiveArmour = _playerData.armour / (1 + _playerData.armour * 0.05f);
                    var damage = Mathf.Max(0, damageMonster - effectiveArmour);
                    
                    player.currentHP -= (int)Math.Round(damage);
                    
                    if (barEntity != default)
                    barEntity.Get<UpdatePlayerUIEvent>();
                    
                    entity.Del<MonsterAttackEvent>();
                }

                if (player.currentHP <= 0)
                {
                    player.currentHP = 0;
                }
            }
        }
    }
}