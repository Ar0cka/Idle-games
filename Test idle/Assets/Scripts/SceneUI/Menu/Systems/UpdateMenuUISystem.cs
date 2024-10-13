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
        private PlayerData _playerData;
        
        
        public void Run()
        {
            foreach (var menuIndex in _menuFilter)
            {
                ref var textUI = ref _menuFilter.Get1(menuIndex);
                ref var entity = ref _menuFilter.GetEntity(menuIndex);
                    
                textUI.hpText.text = $"Max hp = {_playerData.maxHp}";
                textUI.damageText.text = $"Damage = {_playerData.damage}";
                textUI.attackSpeedText.text = $"Attack speed = {_playerData.attackSpeed}";
                textUI.armorText.text = $"Armour = {_playerData.armour}";
                    
                Debug.Log($"Update menu ui");
                    
                entity.Del<UpdateMenuEvent>();
            }
        }
    }
}