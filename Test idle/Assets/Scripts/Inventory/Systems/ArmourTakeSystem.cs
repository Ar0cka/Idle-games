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
        private readonly EcsFilter<TextMenuComponent> _menuFilter = null;
        private PlayerData _playerData;

        public void Run()
        {
            foreach (var itemIndex in _itemFilter)
            {
                ref var item = ref _itemFilter.Get1(itemIndex);
                ref var entity = ref _itemFilter.GetEntity(itemIndex);
                
                _playerData.EquipItem(item._equipItem);

                SendUpdateMenuEvent();
                entity.Destroy();
            }
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