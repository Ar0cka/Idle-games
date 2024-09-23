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
                ref var entity = ref _playerFilter.GetEntity(playerIndex);

                player.currentHP = player.playerSettings._hitPoint;
                entity.Get<UpdateUIEvent>();
                
                ButtonInteractable();
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