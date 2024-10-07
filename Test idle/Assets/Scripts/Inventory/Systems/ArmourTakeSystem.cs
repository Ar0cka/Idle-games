using Inventory.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Inventory.Systems
{
    public class ArmourTakeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ArmourActionEvent> _itemFilter = null;
        private InventorySettings _inventorySettings;
        private InventoryEquip _inventoryEquip;

        public void Run()
        {
            foreach (var itemIndex in _itemFilter)
            {
                ref var itemData = ref _itemFilter.Get1(itemIndex);
                ref var entity = ref _itemFilter.GetEntity(itemIndex);

                var item = _inventorySettings.GetItemFromSlot(itemData._slotData);
                _inventorySettings.UseItemFromSlot(itemData._slotData);
                _inventoryEquip.AddNewItemToEquipSlot(item, itemData._equipItem.slotType);
                
                Debug.Log("Add qeuip item");
                
                entity.Destroy();
            }
        }
    }
}