using DefaultNamespace.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.Battle.Components.Events.BlockAttackEvents
{
    sealed class SerializeAttackCooldownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerCooldownComponent, PlayerSettingsComponent, 
            SerializeAttackCooldownEvent> _playerEcsFilter = null;

        private PlayerData _playerData;

        public void Run()
        {
            foreach (var playerIndex in _playerEcsFilter)
            {
                ref var _cooldown = ref _playerEcsFilter.Get1(playerIndex);
                ref var entity = ref _playerEcsFilter.GetEntity(playerIndex);

                _cooldown.Timer = _playerData.attackSpeed;
                
                Debug.Log($"Serialize cooldown attack event");

                entity.Del<SerializeAttackCooldownEvent>();
            }
        }
    }
}