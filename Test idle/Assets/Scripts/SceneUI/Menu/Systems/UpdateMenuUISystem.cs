using DefaultNamespace.Components;
using DefaultNamespace.SceneUI.Menu.Component;
using Leopotam.Ecs;
using SceneUI.Menu.Events;
using UnityEngine;

namespace DefaultNamespace.SceneUI.Menu.Systems
{
    public class UpdateMenuUISystem : IEcsRunSystem
    { 
        private readonly EcsFilter<TextMenuComponent, UpdateMenuEvent> _menuFilter = null;
        private readonly EcsFilter<PlayerSettingsComponent> _playerFilter = null;
        
        
        public void Run()
        {
            foreach (var menuIndex in _menuFilter)
            {
                ref var textUI = ref _menuFilter.Get1(menuIndex);
                ref var entity = ref _menuFilter.GetEntity(menuIndex);

                foreach (var playerIndex in _playerFilter)
                {
                    ref var playerSettings = ref _playerFilter.Get1(playerIndex).playerSettings;
                    
                    textUI.hpText.text = $"Max hp = {playerSettings._hitPoint}";
                    textUI.damageText.text = $"Damage = {playerSettings._damage}";
                    textUI.attackSpeedText.text = $"Attack speed = {playerSettings._attackSpeed}";
                    textUI.armorText.text = $"Armour = {playerSettings._armour}";
                    
                    Debug.Log($"Update menu ui");
                    
                    entity.Del<UpdateMenuEvent>();
                }
            }
        }
    }
}