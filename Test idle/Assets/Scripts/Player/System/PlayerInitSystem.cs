using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Components;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.Player.System
{
    sealed class PlayerInitSystem : IEcsInitSystem
    {
        private EcsFilter<PlayerSettingsComponent> playerFilter = null;
        private EcsFilter<HpBarComponent> _barFilter = null;

        public void Init()
        {
            foreach (var i in playerFilter)
            {
                ref var playerSettingsComponent = ref playerFilter.Get1(i);

                if (playerSettingsComponent.currentHP == 0)
                {
                    playerSettingsComponent.currentHP = playerSettingsComponent.playerSettings._hitPoint;
                    UpdatePlayerBar();
                }
            }
        }

        private void UpdatePlayerBar()
        {
            foreach (var barIndex in _barFilter)
            {
                ref var barEntity = ref _barFilter.GetEntity(barIndex);
                barEntity.Get<UpdatePlayerUIEvent>();
            }
        }
    }
}