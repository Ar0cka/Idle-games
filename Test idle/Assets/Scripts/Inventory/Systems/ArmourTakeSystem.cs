using Inventory.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Inventory.Systems
{
    public class ArmourTakeSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<ArmourActionEvent> _itemFilter = null;
        private InventorySettings _inventorySettings;
        private InventoryEquip _inventoryEquip;

        public void Run()
        {
            foreach (var itemIndex in _itemFilter)
            {
                ref var itemData = ref _itemFilter.Get1(itemIndex);
                ref var entity = ref _itemFilter.GetEntity(itemIndex);

                if (!_inventoryEquip.GetSlotData(itemData._equipItem.slotType).isOccupied)
                {
                    var ecsEntity = _ecsWorld.NewEntity();
                    var item = _inventorySettings.GetItemFromSlot(itemData._slotData);
                    _inventoryEquip.AddNewItemToEquipSlot(itemData._equipItem.slotType, ecsEntity, itemData._slotData);

                    Debug.Log("Add qeuip item");
                }
                
                else if (_inventoryEquip.GetSlotData((itemData._equipItem.slotType)).isOccupied)
                {
                    var ecsEntity = _ecsWorld.NewEntity();
                    _inventoryEquip.ChangeItemToEquipSlot(itemData._equipItem.slotType, itemData._slotData, ecsEntity);
                    
                    Debug.Log("Change item");
                }
                

                entity.Destroy();
            }
        }
    }
}