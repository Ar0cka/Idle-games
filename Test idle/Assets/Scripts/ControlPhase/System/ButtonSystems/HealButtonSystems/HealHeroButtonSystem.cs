using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Components;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using Unity.VisualScripting;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class HealHeroButtonSystem : IEcsInitSystem
    {
        private readonly EcsFilter<PlayerSettingsComponent> _playerFilter = null;
        private readonly EcsFilter<HpBarComponent> _barFilter = null;
        private readonly EcsFilter<ButtonBattleComponent> _buttonFilter = null;

        public void Init()
        {
            foreach (var buttonIndex in _buttonFilter)
            {
                ref var healButton = ref _buttonFilter.Get1(buttonIndex).healButton;
                ref var entityButton = ref _buttonFilter.GetEntity(buttonIndex);
                entityButton.Get<CheckCooldownHpHealComponent>().cooldownButton = 10f;
                
                healButton.onClick.AddListener(HealPlayer);
            }
        }

        private void HealPlayer()
        {
            foreach (var playerIndex in _playerFilter)
            {
                ref var player = ref _playerFilter.Get1(playerIndex);

                player.currentHP = player.playerSettings._hitPoint;
               
                UpdateUI();
                
                ButtonInteractable();
            }
        }

        private void UpdateUI()
        {
            foreach (var barIndex in _barFilter)
            {
                ref var barEntity = ref _barFilter.GetEntity(barIndex);
                barEntity.Get<UpdatePlayerUIEvent>();
            }
        }
        
        private void ButtonInteractable()
        {
            foreach (var buttonIndex in _buttonFilter)
            {
                ref var healButton = ref _buttonFilter.Get1(buttonIndex).healButton;
                ref var buttonEntity = ref _buttonFilter.GetEntity(buttonIndex);

                healButton.interactable = false;
                buttonEntity.Get<CooldownButtonEvent>();
            }
        }
    }
}