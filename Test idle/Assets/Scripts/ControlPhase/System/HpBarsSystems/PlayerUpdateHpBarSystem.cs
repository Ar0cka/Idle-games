using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Components;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using UnityEngine.PlayerLoop;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class PlayerUpdateHpBarSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HpBarComponent, UpdatePlayerUIEvent> _uiFilter = null;
        private readonly EcsFilter<PlayerSettingsComponent> _player = null;

        public void Run()
        {
            foreach (var playerIndex in _player)
            {
                ref var playerHp = ref _player.Get1(playerIndex).currentHP;
                
                foreach (var uiIndex in _uiFilter)
                {
                    ref var playerHpBar = ref _uiFilter.Get1(uiIndex)._hpPlayer;
                    ref var entity = ref _uiFilter.GetEntity(playerIndex);

                    playerHpBar.text = playerHp.ToString();
                    entity.Del<UpdatePlayerUIEvent>();
                }
            }
        }
    }
}