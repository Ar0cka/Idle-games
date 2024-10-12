using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Components;
using DefaultNamespace.ControlPhase.Components.Events;
using DefaultNamespace.SceneUI.Menu.Component;
using Leopotam.Ecs;
using SceneUI.Menu.Events;
using UnityEngine;

namespace DefaultNamespace.Player.System
{
    sealed class PlayerInitSystem : IEcsInitSystem
    {
        private EcsFilter<PlayerSettingsComponent> playerFilter = null;
        private EcsFilter<HpBarComponent> _barFilter = null;
        private EcsFilter<TextMenuComponent> _textFilter;

        public void Init()
        {
            foreach (var i in playerFilter)
            {
                ref var playerSettingsComponent = ref playerFilter.Get1(i);

                if (playerSettingsComponent.currentHP == 0)
                {
                    playerSettingsComponent.currentHP = playerSettingsComponent.playerSettings._hitPoint;
                    SendUpdateMenuUIEvent();
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

        private void SendUpdateMenuUIEvent()
        {
            foreach (var textIndex in _textFilter)
            {
                ref var entity = ref _textFilter.GetEntity(textIndex);
                entity.Get<UpdateMenuEvent>();
                Debug.Log("Send update menu text event");
            }
        }
    }
}