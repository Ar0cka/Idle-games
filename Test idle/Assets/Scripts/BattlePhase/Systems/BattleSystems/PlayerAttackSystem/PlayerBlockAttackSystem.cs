using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Components;
using DefaultNamespace.MonsterSpawn.Components;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System.BattleSystem.BlockSystems
{
    public class PlayerBlockAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerCooldownComponent, PlayerBlockEvent> _playerFilter = null;
        
        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var entity = ref _playerFilter.GetEntity(i);
                ref var playerBlockTimer = ref _playerFilter.Get1(i);
                
                playerBlockTimer.Timer -= Time.deltaTime;
                
                if (playerBlockTimer.Timer <= 0)
                { 
                    entity.Del<PlayerBlockEvent>();
                }
            }
        }
    }
}
