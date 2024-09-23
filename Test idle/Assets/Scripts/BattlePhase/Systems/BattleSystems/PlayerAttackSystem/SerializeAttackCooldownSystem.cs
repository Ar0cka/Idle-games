using DefaultNamespace.Components;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.Components.Events.BlockAttackEvents
{
    sealed class SerializeAttackCooldownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerCooldownComponent, PlayerSettingsComponent, 
            SerializeAttackCooldownEvent> _playerEcsFilter = null;

        public void Run()
        {
            foreach (var playerIndex in _playerEcsFilter)
            {
                ref var _cooldown = ref _playerEcsFilter.Get1(playerIndex);
                ref var _player = ref _playerEcsFilter.Get2(playerIndex);
                ref var entity = ref _playerEcsFilter.GetEntity(playerIndex);

                _cooldown.Timer = _player.playerSettings._attackSpeed;

                entity.Del<SerializeAttackCooldownEvent>();
            }
        }
    }
}