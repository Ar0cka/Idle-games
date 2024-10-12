using DefaultNamespace.Components;
using DefaultNamespace.SceneUI.Menu.Component;
using Inventory.Events;
using Leopotam.Ecs;
using SceneUI.Menu.Events;
using Scriptable_object.Items;
using UnityEngine;

namespace Inventory.Systems
{
    public class ArmourTakeSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<ArmourActionEvent> _itemFilter = null;
        private readonly EcsFilter<PlayerSettingsComponent> _playerFilter = null;
        private readonly EcsFilter<TextMenuComponent> _menuFilter = null;

        public void Run()
        {
            foreach (var itemIndex in _itemFilter)
            {
                ref var item = ref _itemFilter.Get1(itemIndex);
                ref var entity = ref _itemFilter.GetEntity(itemIndex);
                
                foreach (var playerIndex in _playerFilter)
                {
                    ref var player = ref _playerFilter.Get1(playerIndex);
                    var playerData = player.playerSettings;
                    
                    if (item._equipItem is HairItemStats hairArmour)
                        EquipHairItem(hairArmour, playerData);
                    else if (item._equipItem is Armour armour)
                        EquipArmour(armour, playerData);
                        
                    
                    SendUpdateMenuEvent();
                    entity.Destroy();
                }
            }
        }

        private void EquipHairItem(HairItemStats hairItemStats, PlayerSettings player)
        {
            player.UpgradeArmour(hairItemStats.armour);
            player.UpgradeHitPoint(hairItemStats.hp); 
        }

        private void EquipArmour(Armour armour, PlayerSettings player)
        {
            player.UpgradeArmour(armour.armour);
        }

        private void SendUpdateMenuEvent()
        {
            foreach (var menuIndex in _menuFilter)
            {
                ref var menuEntity = ref _menuFilter.GetEntity(menuIndex);
                menuEntity.Get<UpdateMenuEvent>();
            }
        }
    }
}